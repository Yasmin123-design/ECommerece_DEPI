using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


namespace E_Commerece.Services.Interfaces
{
	public interface IUserService
	{
		Task<IdentityResult> RegisterAsync(RegisterVM register);
		Task<bool> LoginAsync(LoginVM login);
		Task LogOut();
		AuthenticationProperties GetGoogleLoginProperties(string redirectUri);

		AuthenticationProperties ChallengeFacebookLoginAsync(string url);
		Task<(string userEmail, string userName)> HandleGoogleResponse();
		Task<IdentityResult> SaveDataComeFromGoogleOrFaceBook(string username, string email, string provider, string providerKey );

		Task<bool> SendPasswordResetEmailAsync(string email);
		Task<bool> ResetPasswordAsync(RestPasswordVM model);

		Task<string> GetUserRoleAsync(string email);
        int GetUserCount();

    }
}
