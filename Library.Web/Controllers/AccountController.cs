using Library.Model.Models;
using Library.Service;
using Library.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IStaffReaderService _staffReaderService;
		private readonly ILogService _logService;
		public AccountController
		(
			ILogService logService,
			IUserService userService,
			IStaffReaderService staffReaderService
		)
		{
			_userService = userService;
			_staffReaderService = staffReaderService;
			_logService = logService;
		}
		public IActionResult Login()
		{
			return View(new UserLoginViewModel());
		}

		[HttpPost]
		public async Task<ActionResult> Login(UserLoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var logUser = await _userService.LoginUserAsync(model.EmailAddress, model.Password);


				if (logUser != null)
				{
					return RedirectToAction("Index", "Home");
				}

				else
				{
					ViewBag.ErrorMessage = "Login Error!";
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

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(User user)
		{
			if (ModelState.IsValid)
			{
				await _userService.AddUserAsync(user);
				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		// ეს რამდენად საჭიროა?
		public IActionResult Register()
		{
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

				await AddNewStaffReader(model);

				await AddNewUser(model);

				return RedirectToAction("Index", "Account");
			}

			return View(model);
		}

		public async Task AddNewStaffReader(RegisterViewModel model)
		{
			// logged row record for creating New StaffReader 
			LogInfo lastLoggedStaff = await _logService.GetLastLogID(new LogInfo() { TableName = "StaffReaders", DateCreated = DateTime.Now, UserID = null });
			await _logService.SaveLogsAsync();

			// Add & Save New StaffReader to DB
			await _staffReaderService.AddStaffReaderAsync
				(
					new StaffReader()
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
					}
				);

			await _staffReaderService.SaveStaffReaderAsync();

			//------------------------------------------------//
		}
		public async Task AddNewUser(RegisterViewModel model)
		{
			LogInfo lastLoggedUser = await _logService.GetLastLogID(new LogInfo() { TableName = "Users", DateCreated = DateTime.Now, UserID = null });
			await _logService.SaveLogsAsync();

			await _userService.AddUserAsync(new User() { UserName = model.Email, Password = model.Password, LogID = lastLoggedUser.LogID });
			await _userService.SaveUserAsync();

			var user = await _userService.GetManyUsersAsync(u => u.UserName == model.Email);

			//lastLoggedUser.UserID => u.UserID;
			await _logService.UpdateLogAsync(lastLoggedUser);
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
