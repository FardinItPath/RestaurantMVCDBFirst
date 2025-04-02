
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using RestaurantMVCCodeFirst.Services;
using RestaurantMVCCodeFirst.View_Model;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            var roles = await _userService.GetRoles(); // Fetch roles from database
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName"); // Send roles to View
            return View();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password, 
                RoleId = model.RoleId
            };

            await _userService.RegisterUser(user);
            return RedirectToAction("Login", "Account");
        }

        [Route("")]
        [HttpGet("login")]
        [Route("Login")]
        public IActionResult Login()
        
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var authenticatedUser = await _userService.AuthenticateUser(model.Username, model.Password);
            if (authenticatedUser == null) 
            {
                ViewBag.ErrorMessage = "Incorrect username or password!";
                return View(model);
            }
            HttpContext.Session.SetString("UserId", authenticatedUser.UserId.ToString());
            HttpContext.Session.SetString("Username", authenticatedUser.Username);
            HttpContext.Session.SetString("UserRole", authenticatedUser.Role.RoleName);
            HttpContext.Session.SetInt32("RoleId", authenticatedUser.RoleId);
            return RedirectToAction("list","menu");

        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login");
        }

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userService.GetUserByUsername(model.UserName);
            if (user == null)
            {
                TempData["Error"] = "Username not found!";
                return View(model);
            }

            TempData["ResetUsername"] = model.UserName;
            return RedirectToAction("ResetPassword");
        }

     
        [HttpGet("reset-password")]
        public IActionResult ResetPassword()
        {
            if (TempData["ResetUsername"] == null)
                return RedirectToAction("ForgotPassword");

            TempData.Keep();
            return View();
        }

      
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var username = TempData["ResetUsername"] as string;
            if (string.IsNullOrEmpty(username)) return RedirectToAction("ForgotPassword");

            model.UserName = username; 
            await _userService.ResetPassword(model);

            return RedirectToAction("Login");
        }
    }
}

