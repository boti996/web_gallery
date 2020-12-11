using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using web_gallery.Services;
using System.Diagnostics;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class UserRegisterModel : PageModel
    {
        [BindProperty(SupportsGet=true)]
        public string? Token { get; set; }

        [BindProperty]
        [Display(Name = "Email address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; } = null!;

        [BindProperty]
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; } = null!;

        [BindProperty]
        [Display(Name = "Confirm password")]
        [Required]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm password doesn't match.")]
        public string PasswordConfirm { set; get; } = null!;

        private readonly TokenService _tokenService;
        private readonly UserManager<Models.Users.User> _userManager;
        private readonly SignInManager<Models.Users.User> _signInManager;
        //private readonly IEmailSender _sender;
        private readonly ILogger<UserRegisterModel> _logger;

        public UserRegisterModel(
            ILogger<UserRegisterModel> logger, 
            UserManager<Models.Users.User> userManager, 
            SignInManager<Models.Users.User> signInManager,
            TokenService tokenService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public IActionResult OnGet(string token)
        {
            Debug.WriteLine($"token: {token}");
            var tokenFromDb = _tokenService.GetByValue(token);
            if (tokenFromDb == null || !Preferences.isValidRegistrationToken(tokenFromDb))
            {
                return RedirectToPage("/Warning", new {warningMessage=WarningMessages.RegistrationTokenInvalid});
            }
            Debug.WriteLine("Register token was valid.");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            /* TODO: ??
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                                          .ToList();
            */
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Warning", new { warningMessage=string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)),
                                        returnUrl=$"/Users/Register?token={Token}"});
            }
            Debug.WriteLine($"User {Email} is being created..");
            var registeredUser = new Models.Users.User { UserName = Email, Email = Email };
            var registrationResult = await _userManager.CreateAsync(registeredUser, Password);

            if (registrationResult.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                /* TODO: Send confirmation email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = uregisteredUserser.Id, code = code },
                protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", 
                                        new { email = Email });
                }
                else
                {
                    await _signInManager.SignInAsync(registeredUser, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                */

                await _signInManager.SignInAsync(user: registeredUser, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in registrationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
