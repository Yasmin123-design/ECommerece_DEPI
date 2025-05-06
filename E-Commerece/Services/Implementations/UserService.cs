using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace E_Commerece.Services.Implementations
{
	public class UserService : IUserService
	{
        private string GenerateSafeUsername(string name)
        {
            // نشيل كل الحروف الغير مسموح بيها ونخلي الاسم lowercase
            var cleaned = Regex.Replace(name ?? "", @"[^a-zA-Z0-9]", "").ToLower();

            // لو الاسم بقى فاضي أو قصير جدًا، نضيف عليه حاجة تضمن التفرد
            if (string.IsNullOrEmpty(cleaned) || cleaned.Length < 3)
            {
                cleaned = "user_" + Guid.NewGuid().ToString("N").Substring(0, 6);
            }

            return cleaned;
        }


        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> User;
		private readonly SignInManager<User> Sign;
		private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _unitOfWork;
        public UserService(UserManager<User> user ,
			SignInManager<User> sign ,
			IHttpContextAccessor httpContextAccessor , 
			IEmailService emailService , 
			IConfiguration configuration,
			IUnitOfWork unitOfWork
			)
		{
			this.User = user;
			this.Sign = sign;
			this._httpContextAccessor = httpContextAccessor;
			this._emailService = emailService;
			this._configuration = configuration;
			this._unitOfWork = unitOfWork;
		}

		public AuthenticationProperties ChallengeFacebookLoginAsync(string url)
		{
			
		   return new AuthenticationProperties { RedirectUri = url };
		}

		public AuthenticationProperties GetGoogleLoginProperties(string redirectUri)
		{
			return new AuthenticationProperties { RedirectUri = redirectUri };
		}

		public async Task<(string userEmail, string userName)> HandleGoogleResponse()
		{
			var result = await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (result.Principal == null)
				return (null, null);

			var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
			var userEmail = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var userName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

			return (userEmail, userName);
		}

		public async Task<bool> LoginAsync(LoginVM login)
		{
			var user = await User.FindByEmailAsync(login.Email);
			if (user == null)
				return false;
			var result = await Sign.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
			return result.Succeeded;
		}

		public Task LogOut()
		{
			return Sign.SignOutAsync();
		}

		public async Task<IdentityResult> RegisterAsync(RegisterVM register)
		{
			var user = new User
			{
				UserName = register.UserName,
				Address = register.Address,
				PasswordHash = register.Password,
				Email = register.Email
			};
			var result = await User.CreateAsync(user, register.Password);
			if (result.Succeeded)
			{
                // جلب قائمة الإيميلات الإدارية من appsettings.json
                var adminEmails = _configuration.GetSection("AdminSettings:AdminEmails").Get<List<string>>();

                if (adminEmails != null && adminEmails.Contains(register.Email))
                {
                    await User.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await User.AddToRoleAsync(user, "User");
                }
            }
			return result;
		}

        public async Task<IdentityResult> SaveDataComeFromGoogleOrFaceBook(string username, string email, string provider, string providerKey)
        {
            // التحقق إذا المستخدم موجود فعلاً بناءً على الإيميل
            var existingUser = await User.FindByEmailAsync(email);

            if (existingUser != null)
            {
                // إذا كان موجود، نقوم بتسجيل دخوله مباشرة
                await Sign.SignInAsync(existingUser, isPersistent: false);
                return IdentityResult.Success;
            }

            // إنشاء المستخدم الجديد
            var user = new User
            {
                UserName = GenerateSafeUsername(username),  // يمكن التعديل هنا لو عايزة تضمن أن الـ username ما فيهش مسافات أو مشاكل تانية
                Email = email
            };

            // إنشاء الحساب الجديد بدون كلمة مرور
            var result = await User.CreateAsync(user);
            if (!result.Succeeded)
                return result;

            // ربط المستخدم بطريقة تسجيل الدخول الخارجية
            var loginInfo = new UserLoginInfo(provider, providerKey, provider);
            await User.AddLoginAsync(user, loginInfo);

            // إضافة المستخدم إلى الدور "User"
            await User.AddToRoleAsync(user, "User");

            // تسجيل الدخول للمستخدم الجديد بعد إنشائه
            await Sign.SignInAsync(user, isPersistent: false);

            return result;
        }


        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var user = await User.FindByEmailAsync(email);
            if (user == null) return false;

            var token = await User.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"http://localhost:5006/Account/ResetPassword?token={Uri.EscapeDataString(token)}&email={email}";

            await _emailService.SendEmailAsync(email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");
            return true;
        }
        public async Task<bool> ResetPasswordAsync(RestPasswordVM model)
		{
            var user = await User.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await User.ResetPasswordAsync(user, model.Token, model.NewPassword);
				return true;
            }
			return false;

        }

        public async Task<string> GetUserRoleAsync(string email)
        {
            var user = await User.FindByEmailAsync(email);
            if (user == null)
                return null;

            var roles = await User.GetRolesAsync(user);
            return roles.FirstOrDefault(); // نفترض أن المستخدم لديه دور واحد فقط
        }

		public int GetUserCount() => this._unitOfWork.Users.GetUserCount();
    }
}
