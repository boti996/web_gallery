using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class UserLoginModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Email address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; } = null!;
        [BindProperty]
        [Display(Name = "Your password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; } = null!;
        [BindProperty]
        [Display(Name = "Remember me")]
        [Required]
        public bool RememberMe { set; get; } = false;

        private readonly ILogger<UserLoginModel> _logger;
        private readonly UserManager<Models.Users.User> _userManager;
        private readonly SignInManager<Models.Users.User> _signInManager;

        public UserLoginModel(
            ILogger<UserLoginModel> logger,
            UserManager<Models.Users.User> userManager,
            SignInManager<Models.Users.User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var suspendedDays = user.checkSuspended();
                    if (suspendedDays > 0)
                    {
                        ModelState.AddModelError(string.Empty, WarningMessages.SuspendedAccount);
                        ModelState.AddModelError(string.Empty, WarningMessages.SuspendedDaysLeft(suspendedDays));
                        return Page();
                    }
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Email,
                                Password, RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new
                    {
                        ReturnUrl = returnUrl,
                        RememberMe = RememberMe
                    });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, WarningMessages.InvalidLoginAttempt);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}
