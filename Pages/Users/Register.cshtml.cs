using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class UserRegisterModel : PageModel
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
        [Display(Name = "Confirm your password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match.")]
        public string PasswordConfirm { set; get; } = null!;

        private readonly UserManager<Models.Users.User> _userManager;
        private readonly SignInManager<Models.Users.User> _signInManager;
        //private readonly IEmailSender _sender;
        private readonly ILogger<UserRegisterModel> _logger;

        public UserRegisterModel(
            ILogger<UserRegisterModel> logger, 
            UserManager<Models.Users.User> userManager, 
            SignInManager<Models.Users.User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            /* TODO: ??
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                                          .ToList();
            */
            if (ModelState.IsValid)
            {
                return Page();
            }

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
