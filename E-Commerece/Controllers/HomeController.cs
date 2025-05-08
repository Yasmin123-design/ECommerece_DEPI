using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
	public class HomeController : Controller
	{
		private readonly INewsletterSubscribersService _newsletterSubscribersService;
		public HomeController(INewsletterSubscribersService newsletterSubscribersService)
		{
			this._newsletterSubscribersService = newsletterSubscribersService;
		}
		public IActionResult PrivacyPolicy()
		{
			return View();
		}

		public IActionResult TermsAndConditions()
		{
			return View();
		}
		[Authorize]
		public IActionResult SubscribeEmail(string email)
		{
			try
			{
				// Validate email format
				if (!IsValidEmail(email))
				{
					return View("SubscriptionError", "Invalid email format");
				}

				// Check if email already exists
				if (_newsletterSubscribersService.EmailExists(email))
				{
					return View("AlreadySubscribed", email);
				}

				// Create new subscription
				_newsletterSubscribersService.CreateNew(email);
				_newsletterSubscribersService.SaveChange();

				return View("SubscribeEmail",email);
			}
			catch (Exception ex)
			{
				return View("SubscriptionError", "An error occurred while processing your subscription");
			}
		}
		public IActionResult AlreadySubscribed()
		{
			return View();
		}
		public IActionResult SubscriptionError()
		{
			return View();
		}
        private bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

	}
}
