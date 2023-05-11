using Library.Data.Repositories;
using LibraryManagementSystem.Models.Account;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Service;

namespace LibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _loginUser;
        private readonly UserService _userService;

        public AccountController(IUserRepository logUser, UserService userService)
        {
            _loginUser = logUser;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var isSuccess = await _userService.LoginUser(model.EmailAddress, model.Password);


            //var isSuccess = await _loginUser.AuthenticateUser(model.EmailAddress, model.Password);

            if (isSuccess != null)
            {
                ViewBag.username = string.Format("Successfully logged-in", model.EmailAddress);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.username = string.Format("Login failed", model.EmailAddress);
                return View(model);
            }
        }


        //.private readonly UserManager<IdentityUser> _userManager;
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

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await _userManager.FindByEmailAsync(model.Email);

            //    if (user != null)
            //    {
            //        TempData["Error"] = "the same account is already exists!";
            //        return View(model);
            //    }

            //    var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };
            //    var result = await _userManager.CreateAsync(newUser,model.Password);

            //    if (result.Succeeded) 
            //    { 
            //        await _signInManager.SignInAsync(newUser, isPersistent: false);
            //        return RedirectToAction("index", "home");
            //    }

            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //    }
            //}
            return View(model);
        }
    }
}
