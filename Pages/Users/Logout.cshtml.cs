using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace web_gallery.Pages
{
    public class UserLogoutModel : PageModel
    {
        private readonly SignInManager<Models.Users.User> _signInManager;
        private readonly ILogger<UserLogoutModel> _logger;

        public UserLogoutModel(SignInManager<Models.Users.User> signInManager, ILogger<UserLogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync() {
            return await OnPostAsync(Preferences.DefaultRedirectUrl);
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }

    }
}
