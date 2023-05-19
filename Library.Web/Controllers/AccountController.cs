using Library.Data.Repositories;
using Library.Model;
using Library.Model.Models;
using Library.Service;
using Library.Web.Models.Account;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Library.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.LoginUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserID)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
