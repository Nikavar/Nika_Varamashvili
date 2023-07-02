using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Library.Web.Models.Account;
using Mapster;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Text;
using Library.Web.Constants;
using Humanizer.Localisation;
using DocumentFormat.OpenXml.Spreadsheet;

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
		private readonly ITokenService _tokenService;
		private readonly IConfiguration _configuration;
		
		public AccountController
        (
            ILogService logService,
            IUserService userService,
            IStaffReaderService staffReaderService,
            IRoleService roleService,
            IRoleUserService roleUserService,
            IPositionService positionService,
            ITokenService tokenService,
			IConfiguration configuration
        )
        {
            _logService = logService;
            _userService = userService;
            _staffReaderService = staffReaderService;
            _roleService = roleService;
            _roleUserService = roleUserService;
            _positionService = positionService;
            _tokenService = tokenService;
			_configuration = configuration;
        }
        public IActionResult Login()
		{
			return View(new UserLoginViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var loggedUser = await _userService.LoginUserAsync(model.EmailAddress, model.Password);
                if (loggedUser != null)
				{
					var token = Helper.TokenGeneration(loggedUser, _configuration);
                    HttpContext.Response.Cookies.Append("Token", token);
                    return RedirectToAction("Index", "Home");
				}

				else
				{
					TempData["Error"] = Warnings.UsernameOrPasswordIsIncorrect;
					return View("Login");
				}
			}
			return View("Login");
		}

        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            return RedirectToAction("Index", "Home");
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

		public async Task<IActionResult> Register()
		{
			var positions = await _positionService.GetAllPositionsAsync();
			List<RegisterViewModel> positionLst = new List<RegisterViewModel>();

			//var list = (from p in positions select p.PositionName).ToList();

			//list.Insert(0, "--- Select Positions ---");
			//ViewBag.message = list;

            ViewBag.message = positions;
            return View(new RegisterViewModel());
		}


		private void SendEmail(string body, string email)
		{
			// create client
			var client = new HttpClient();
		}

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if (ModelState.IsValid)
			{
				var isUserRegistered = await _userService.LoginUserAsync(model.Email, model.Password);

				if (isUserRegistered != null)
				{
					ViewBag.ErrorMessage = Warnings.UserIsAlreadyRegistered;
					return View("Index", "Home");
				}

				// Add & Log 'User' Entity In Database
				var userEntity = model.Adapt<User>();
                await Helper.AddEntityWithLog(userEntity, _userService.AddUserAsync,_logService);		

                // Add & Log 'StaffReader' Entity In Database
                var staffReaderEntity = model.Adapt<StaffReader>();
                await Helper.AddEntityWithLog(staffReaderEntity, _staffReaderService.AddStaffReaderAsync, _logService);

                // Update User Entity 'StaffReaderID' property
                userEntity.StaffReaderID = staffReaderEntity.ID;
				await _userService.UpdateUserAsync(userEntity);

                // Assing Role of 'user' to new user by default
                var role = _roleService.GetRoleByNameAsync(Roles.user.ToString());
				var roleUserEntity = new RoleUser() { RoleID = role.ID, UserID = userEntity.id };
                await Helper.AddEntityWithLog(roleUserEntity,_roleUserService.AddRoleUserAsync,_logService);

                // Logged PositionStaff
                var loggedPositionStaff = await _logService.AddLogAsync(
						new LogInfo 
						{ 
							TableName= "PositionStaff",
						}					
					);

                if (userEntity != null)
                {
                    var token = Helper.TokenGeneration(staffReaderEntity.ID.ToString(), _configuration);
                    var url = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Account", new { token = token });
                    await Helper.EmailLinkConfirmation(userEntity.Email, url, _configuration, staffReaderEntity);
                }

                return RedirectToAction("Index", "Home");
			}

			return View("Error","Account"); 
		}

        public async Task<ActionResult> ConfirmEmail(string token)
        {
            var id = Helper.TokenDecryption(token);
			var staffReader = await _staffReaderService.GetStaffReaderByIdAsync(id);
           
            if (staffReader == null)
            {
                ViewBag.ErrorMessage = "Some Error";
                return View("Index", "Home");
            }

            staffReader.IsConfirmed = true;

			await _staffReaderService.UpdateStaffReaderAsync(staffReader);

			await Helper.UpdateEntityWithLog(staffReader, _staffReaderService.UpdateStaffReaderAsync, _logService);

            ViewBag.ErrorMessage = Warnings.EmailWasConfirmed;

            return View();
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
				await Helper.UpdateEntityWithLog(user, _userService.UpdateUserAsync, _logService);
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
			await _logService.DeleteManyLogsAsync(x=> x.EntityID == user.id);
			return RedirectToAction(nameof(Index));
		}
    }
}
