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
using WebMatrix.WebData;
using Windows.ApplicationModel.Email;

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

		public ActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel model)		
		{
			if (ModelState.IsValid)
			{
				var entity = await _userService.GetManyUsersAsync(x => x.Email == model.Email);
				if (entity != null)
				{
					string To = model.Email, ToMailText, Password, SMTPPort, Host;
					string token = Helper.TokenGeneration(model.Email, _configuration);

					if (token == null)
					{
						// If user does not exist or is not confirmed.
						ViewBag.ErrorMessage = Warnings.UserNotExistsOrNotConfirmed;
						return View("Index");
					}
					else
					{
						//Create URL with above token
						var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = model.Email, code = token }, "http") + "'>Reset Password</a>";
						//HTML Template for Send email
						string subject = "Your changed password";
						string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
						//Get and set the AppSettings using configuration manager.
						Helper.AppSettings(out ToMailText, out Password, out SMTPPort, out Host, _configuration);
						//Call send email methods.
						string From = _configuration.GetSection("MailSettings:From").Value;
						Helper.SendEmail(From, subject, body, To, ToMailText, Password, SMTPPort, Host);
					}
				}
			}
			return View();
		}

        public ActionResult ResetPassword(string code)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Token = code;
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool resetResponse = WebSecurity.ResetPassword(model.Token, model.Password);
                if (resetResponse)
                {
                    ViewBag.Message = Warnings.SuccesfullyChanged;
                }
                else
                {
                    ViewBag.Message = Warnings.SomethingWasWrong;
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(string token)
        {
            var id = Helper.TokenDecryption(token);
            var staffReader = await _staffReaderService.GetStaffReaderByIdAsync(id);

            if (staffReader == null)
            {
                ViewBag.ErrorMessage = Warnings.SomethingWasWrong;
                return View("Index", "Home");
            }

            staffReader.IsConfirmed = true;
            await _staffReaderService.UpdateStaffReaderAsync(staffReader);
            await Helper.UpdateEntityWithLog(staffReader, _staffReaderService.UpdateStaffReaderAsync, _logService);
            ViewBag.ErrorMessage = Warnings.EmailWasConfirmed;

            return View();
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
                // Add & Log 'StaffReader' Entity In Database
                var staffReaderEntity = model.Adapt<StaffReader>();
                await Helper.AddEntityWithLog(staffReaderEntity, _staffReaderService.AddStaffReaderAsync, _logService);

				// Add & Log 'User' Entity In Database
				var userEntity = model.Adapt<User>();
                userEntity.StaffReaderID = staffReaderEntity.ID;
                await Helper.AddEntityWithLog(userEntity, _userService.AddUserAsync,_logService);		

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

                    await Helper.EmailLinkConfirmation(userEntity.Email, url, staffReaderEntity, _configuration);
                }

                return RedirectToAction("Index", "Home");
			}

			return View("Error","Account"); 
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
