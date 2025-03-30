using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Facebook;
using E_Commerece.Services.Implementations;

namespace E_Commerece.Controllers
{
    public class AccountController : Controller
    {
        public IUserService UserService { get; set; }
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await UserService.RegisterAsync(model);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var isSucceed = await UserService.LoginAsync(model);
            if (!isSucceed)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // ✅ جلب دور المستخدم بعد تسجيل الدخول
            var userRole = await UserService.GetUserRoleAsync(model.Email);

            if (userRole == "Admin")
            {
                return RedirectToAction("Admin");  
            }
            else
            {
                return RedirectToAction("Index", "Product"); 
                    
            }
        }
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult LoginWithGoogle()
        {
            var properties = UserService.GetGoogleLoginProperties(Url.Action("GoogleResponse"));
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> LoginWithFacebook()
        {
            var properties = UserService.ChallengeFacebookLoginAsync(Url.Action("ExternalLoginCallback", "Account", null, Request.Scheme));
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);

            if (result.Principal == null)
                return RedirectToAction("Login");
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var userEmail = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


            // استخراج معلومات تسجيل الدخول الخارجي
            var externalLogin = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            var provider = result.Properties.Items.ContainsKey(".AuthScheme") ? result.Properties.Items[".AuthScheme"] : "Facebook";

            var providerKey = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userEmail != null)
            {
                var resultSave = await UserService.SaveDataComeFromGoogleOrFaceBook(userName, userEmail, provider, providerKey);
                if (resultSave.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (result.Principal == null)
                return RedirectToAction("Login");

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var userEmail = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // استخراج معلومات تسجيل الدخول الخارجي
            var externalLogin = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var provider = result.Properties.Items[".AuthScheme"];
            var providerKey = externalLogin.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userEmail != null)
            {
                var resultSave = await UserService.SaveDataComeFromGoogleOrFaceBook(userName, userEmail, provider, providerKey);
                if (resultSave.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await UserService.SendPasswordResetEmailAsync(model.Email);
            if (!result)
                return RedirectToAction("ForgotPasswordConfirmation");
            return RedirectToAction("ForgotPasswordConfirmation");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new RestPasswordVM { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(RestPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await UserService.ResetPasswordAsync(model);
            if (result)
                return RedirectToAction("ResetPasswordConfirmation");

            ModelState.AddModelError("", "Invalid password reset request.");
            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult Logout()
        {
            UserService.LogOut();
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return RedirectToAction("Index", "Product");
        }

    }
}
