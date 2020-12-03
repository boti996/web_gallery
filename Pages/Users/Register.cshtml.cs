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
        [Display(Name = "Your password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; } = null!;
        [Display(Name = "Confirm your password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match.")]
        public string PasswordConfirm { set; get; } = null!;

        private readonly UserManager<Models.Users.User> _userManager;
        private readonly SignInManager<Models.Users.User> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public UserRegisterModel(
            ILogger<IndexModel> logger, 
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registeredUser = new Models.Users.User { UserName = Email, Email = Email };
            var registrationResult = await _userManager.CreateAsync(registeredUser, Password);

            if (registrationResult.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                //Send confirmation email
                //var code = await userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
                //var callbackUrl = Url.EmailConfirmationLink(registeredUser.Id.ToString(), code, Request.Scheme);
                //await emailSender.SendEmailAsync(Email, callbackUrl, "");

                await _signInManager.SignInAsync(user: registeredUser, isPersistent: false);
            }

            return RedirectToPage("./Index");
        }
    }
}
