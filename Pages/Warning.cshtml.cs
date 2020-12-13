using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class WarningModel : PageModel
    {
        [BindProperty]
        public string? ReturnUrl { get; set; }
        [BindProperty]
        public string? WarningMessage { get; set; }
        private readonly ILogger<WarningModel> _logger;

        public WarningModel(ILogger<WarningModel> logger)
        {
            _logger = logger;
        }

        //[HttpGet("{warningMessage}/{returnUrl}")]
        public void OnGet(string? warningMessage, string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Preferences.DefaultRedirectUrl;
            WarningMessage = warningMessage ?? WarningMessages.Default;
            var warning = $"Warning/Information message was occured: {WarningMessage}";
            Debug.WriteLine(warning);
            _logger.LogInformation(warningMessage);
        }
    }
}
