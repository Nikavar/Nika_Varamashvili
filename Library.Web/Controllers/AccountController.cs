﻿using Library.Model.Models;
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
using Humanizer.Localisation;
using DocumentFormat.OpenXml.Spreadsheet;
using WebMatrix.WebData;
using Windows.ApplicationModel.Email;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.InteropServices;

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
		private readonly IEmailService _emailService;
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
            IEmailService emailService,
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
			_emailService = emailService;
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
					var token = TokenMethods.TokenGeneration(loggedUser, _configuration);
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
					string token = TokenMethods.TokenGeneration(model.Email, _configuration);

					if (token == null)
					{
						// If user does not exist or is not confirmed.
						ViewBag.ErrorMessage = Warnings.UserNotExistsOrNotConfirmed;
						return View("Index");
					}
					else
					{
                      
                        var url = Request.Scheme + "://" + Request.Host + Url.Action("ResetPassword", "Account", new { email = model.Email, code = token }, "http") + "'>Reset Password</a>";
						int staffReaderId = (int)entity.FirstOrDefault().StaffReaderID;

						var staffReader = await _staffReaderService.GetStaffReaderByIdAsync(staffReaderId);
					    await EmailMethods.SendEmailTemplateAsync<ForgetPasswordViewModel> (model, _emailService, _configuration, staffReader);
					}
				}
			}
			return View("ForgetPassword");
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
            var id = TokenMethods.TokenDecryption(token);
            var staffReader = await _staffReaderService.GetStaffReaderByIdAsync(id);

            if (staffReader == null)
            {
                ViewBag.ErrorMessage = Warnings.SomethingWasWrong;
                return View("Index", "Home");
            }

            staffReader.IsConfirmed = true;
            await _staffReaderService.UpdateStaffReaderAsync(staffReader);
            await EntityMethods.UpdateEntityWithLog(staffReader, _staffReaderService.UpdateStaffReaderAsync, _logService);
            ViewBag.ErrorMessage = Warnings.EmailWasConfirmed;

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index(string sortBy, string find = "", int pg = 1)
		{
			var users = await _userService.GetAllUsersAsync();
			pg = pg < 1 ? 1 : pg;

			find = string.IsNullOrEmpty(find) ? string.Empty : find.ToLower();
			int recsCount = users.Where(x=>x.Email.ToLower().StartsWith(find)).Count();

			var model = new PagerModel(recsCount, pg, StaticParameters.pageSize);
			int recSkip = (pg - 1) * StaticParameters.pageSize;

			var data = users.Skip(recSkip).Take(model.PageSize).ToList();
			data = data.FindAll(x => x.Email.ToLower().StartsWith(find));

			this.ViewBag.Pager = model;

			ViewBag.SortEmailParameter = string.IsNullOrEmpty(sortBy) ? "Email desc" : "";
			ViewBag.SortPasswordParameter = sortBy == "Password" ? "Password desc" : "Password";

			switch (sortBy)
			{
				case "Email desc":
					data = data.OrderByDescending(x => x.Email).ToList();
					break;

				case "Password desc":
					data = data.OrderByDescending(x => x.Password).ToList();
					break;

				case "Password":
					data = data.OrderBy(x => x.Password).ToList();
					break;

				default:
					data = data.OrderBy(x => x.Email).ToList();
					break;
			}

			return View(data);
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
                await EntityMethods.AddEntityWithLog(staffReaderEntity, _staffReaderService.AddStaffReaderAsync, _logService);

				// Add & Log 'User' Entity In Database
				var userEntity = model.Adapt<User>();
                userEntity.StaffReaderID = staffReaderEntity.ID;
                await EntityMethods.AddEntityWithLog(userEntity, _userService.AddUserAsync,_logService);		

                // Assing Role of 'user' to new user by default
                var role = _roleService.GetRoleByNameAsync(AccountRole.user.ToString());
				var roleUserEntity = new RoleUser() { RoleID = role.ID, UserID = userEntity.id };
                await EntityMethods.AddEntityWithLog(roleUserEntity,_roleUserService.AddRoleUserAsync,_logService);

                // Logged PositionStaff
                var loggedPositionStaff = await _logService.AddLogAsync(
						new LogInfo 
						{ 
							TableName= "PositionStaff",
						}					
					);

                if (userEntity != null)
                {
                    //Create URL with above token
                    var token = TokenMethods.TokenGeneration(staffReaderEntity.ID.ToString(), _configuration);
                    var url = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Account", new { email = model.Email, code = token }, "http") + "'>Confirm Password</a>";

                    await EmailMethods.SendEmailTemplateAsync<RegisterViewModel>(model, _emailService, _configuration);
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
				await EntityMethods.UpdateEntityWithLog(user, _userService.UpdateUserAsync, _logService);
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
