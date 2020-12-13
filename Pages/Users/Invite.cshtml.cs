using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using web_gallery.Services;
using System.Threading.Tasks;

namespace web_gallery.Pages
{
    [Authorize(Policy = PolicyNames.RequireAdminRole)]
    public class UserInviteModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Email address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; } = null!;
        private readonly ILogger<UserInviteModel> _logger;
        private readonly IEmailService _emailService;
        private readonly TokenService _tokenService;

        public UserInviteModel(
            ILogger<UserInviteModel> logger,
            IEmailService emailService,
            TokenService tokenService
        )
        {
            _logger = logger;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = _tokenService.Create(
                TokenService.generateToken()
            );
            await _emailService.sendEmailMessage(
                Email,
                MessageTemplates.InviteMessage(token)
            );

            return RedirectToPage(
                "/Warning",
                new {
                    warningMessage = WarningMessages.InvitationSentOutSuccessfully
                }
            );
        }
    }
}
