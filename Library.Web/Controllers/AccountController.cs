using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Office2019.Excel.ThreadedComments;
using DocumentFormat.OpenXml.Wordprocessing;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text.Json;

namespace Library.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IStaffReaderService _staffReaderService;
		private readonly IRoleService _roleService;
		private readonly IRoleUserService _roleUserService;
		private readonly ILogService _logService;
		private readonly IPositionService _positionService;
		public AccountController
		(
			ILogService logService,
			IUserService userService,
			IStaffReaderService staffReaderService,
			IRoleService roleService,
			IRoleUserService roleUserService,
			IPositionService positionService
		)
		{
			_logService = logService;
			_userService = userService;
			_staffReaderService = staffReaderService;
			_roleService = roleService;
			_roleUserService = roleUserService;
			_positionService = positionService;
		}
		public IActionResult Login()
		{
			return View(new UserLoginViewModel());
		}

		[HttpPost]
		public IActionResult Login(UserLoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var logUser = _userService.LoginUser(model.EmailAddress, model.Password);

				if (logUser != null)
				{
					return RedirectToAction("Index", "Home");
				}

				else
				{
                    TempData["Error"] = "username or password is incorrect!";
					return View("Login");
				}
			}
			return View("Login");
		}

		public async Task<IActionResult> Index()
		{
			var users = await _userService.GetAllUsersAsync();
			return View(users);
		}

		public async Task<IActionResult> Details(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			return View(user);
		}

		#region Old Code for Identity
		//public async Task<IActionResult> Login(UserLoginViewModel model)
		//{
		//    //var isSuccess = await _userService.LoginUser(model.EmailAddress, model.Password);


		//    //var isSuccess = await _loginUser.AuthenticateUser(model.EmailAddress, model.Password);

		//    //if (isSuccess != null)
		//    //{
		//    //    ViewBag.username = string.Format("Successfully logged-in", model.EmailAddress);
		//    //    return RedirectToAction("Index", "Home");
		//    //}

		//    //else
		//    //{
		//    //    ViewBag.username = string.Format("Login failed", model.EmailAddress);
		//    //    return View(model);
		//    //}
		//}


		//private readonly UserManager<IdentityUser> _userManager;
		//private readonly SignInManager<IdentityUser> _signInManager;

		//private readonly IUserService userService;

		//public AccountController(IUserService userService)
		//{
		//    this.userService = userService;            
		//}

		//public AccountController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, 
		//                        SignInManager<IdentityUser> signInManager)
		//{
		//    _userManager = userManager;
		//    _signInManager = signInManager;
		//}

		//public IActionResult Login()
		//{
		//    return View(new LoginViewModel());
		//}

		//[HttpPost]
		//public async Task<IActionResult> Login(LoginViewModel model)
		//{
		//    if (!ModelState.IsValid) return View(model);
		//    //var user = await _userManager.FindByEmailAsync(model.EmailAddress);

		//    var user = userService.GetUserById(model.UserId);

		//    if (user != null)
		//    {
		//        //var password = await _userManager.CheckPasswordAsync(user, model.Password);

		//        //userService.CreateUser(user);


		//        //if (password)
		//        //{
		//        //    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
		//        //    if (result.Succeeded)
		//        //    {
		//        //        return RedirectToAction("Index", "home");
		//        //    }

		//        //    TempData["Error"] = "Please, Try Again!";                    
		//        //    return View(model);
		//        //}
		//    }
		//    TempData["Error"] = "Wrong, Try Again";

		//    return View(model);
		//}
		#endregion

		//public IActionResult Create()
		//{
		//	return View();
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create(User user)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		await _userService.AddUserAsync(user);
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(user);
		//}


		public async Task<IActionResult> Register()
		{
			var positions = await _positionService.GetAllPositionsAsync();
			List<RegisterViewModel> positionLst = new List<RegisterViewModel>();

			var list = (from p in positions select p.PositionName).ToList();

			list.Insert(0, "--- Select Positions ---");
			ViewBag.message = list;

			return View(new RegisterViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{

            if (ModelState.IsValid)
			{
				var checkedMail = _userService.CheckUserByMail(model.Email);

				if (checkedMail != null)
				{
					ViewBag.ErrorMessage = "This mail is already exists!";
					return View("Index", "Home");
				}

				var newStaffReaderID = await AddNewStaffReader(model);
				var lastUser = await AddNewUser(model, newStaffReaderID);

				// Logged RoleUser

				var lastLoggedRoleUser = await _logService.AddLogAsync
				(
					new LogInfo 
					{ 
						TableName = "RoleUsers", 
						DateCreated = DateTime.Now 
					}
				);

				// Add RoleUser To DB
				await _roleUserService.AddRoleUserAsync
				(
					new RoleUser 
					{ 
						LogID = lastLoggedRoleUser.LogID, 
						RoleID = 2, 
						UserID = lastUser.id 
					}
				);

				// Logged PositionStaff
				var loggedPositionStaff = await _logService.AddLogAsync(
						new LogInfo 
						{ 
							TableName= "PositionStaff",
							DateCreated = DateTime.Now
						}					
					);

				// To_DO
				//var positionId = await _positionService.GetPositionByIdAsync("Manager");

				return RedirectToAction("Index", "Home");
				//return View();
			}

			return View("Error","Account"); // => exeption + return View()
		}

		private async Task<int> AddNewStaffReader(RegisterViewModel model)
		{
			// logged row record for creating New StaffReader 
			
			var lastLoggedStaff = await _logService.AddLogAsync(new LogInfo() { TableName = "StaffReaders", DateCreated = DateTime.Now, UserID = null });

			// Add & Save New StaffReader to DB
			var newStaffReader = new StaffReader()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				DOB = model.DOB,
				PersonalNumber = model.PersonalNumber,
				PassportNumber = model.PassportNumber,
				PhoneNumber = model.PhoneNumber,
				Email = model.Email,
				Address = model.Address,
				Gender = model.Gender,
				PersonalPhoto = model.PersonalPhoto,
                LogID = lastLoggedStaff.LogID
            };

			try
			{
				string staffReader_jsondata = JsonConvert.SerializeObject(newStaffReader, Formatting.Indented);
                lastLoggedStaff.LogContent = staffReader_jsondata;
                lastLoggedStaff.LogStatus = LogStatus.Info.ToString();

				await _staffReaderService.AddStaffReaderAsync(newStaffReader);

            }
			catch (Exception ex)
			{
				lastLoggedStaff.LogStatus = LogStatus.Error.ToString();
				lastLoggedStaff.LogContent = ex.Message;
			}

			finally
			{
                await _logService.UpdateLogAsync(lastLoggedStaff);
			}

			return newStaffReader.ID;
		}
		private async Task<User> AddNewUser(RegisterViewModel model, int lastStaffReaderID)
		{			
			LogInfo lastLoggedUser = await _logService.AddLogAsync(new LogInfo() { TableName = "Users", DateCreated = DateTime.Now, UserID = null });

			var newUser = new User()
			{
				UserName = model.Email,
				Password = model.Password,
				LogID = lastLoggedUser.LogID,
				StaffReaderID = lastStaffReaderID
			};

            try
            {
                string staffReader_jsondata = JsonConvert.SerializeObject(newUser, Formatting.Indented);
                lastLoggedUser.LogContent = staffReader_jsondata;
                lastLoggedUser.LogStatus = LogStatus.Info.ToString();

                await _userService.AddUserAsync(newUser);
            }
            catch (Exception ex)
            {
                lastLoggedUser.LogStatus = LogStatus.Error.ToString();
                lastLoggedUser.LogContent = ex.Message;
            }

            finally
            {
                await _logService.UpdateLogAsync(lastLoggedUser);
            }

            return newUser;
		}

		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, User user)
		{
			if (id != user.id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _userService.UpdateUserAsync(user);
				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			return View(user);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(User user)
		{
			await _userService.DeleteUserAsync(user);
			return RedirectToAction(nameof(Index));
		}
	}
}
