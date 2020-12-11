using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace web_gallery.Pages
{
    [Authorize(Policy = PolicyNames.RequireAdminRole)]
    public class UserBanModel : PageModel
    {
        private readonly ILogger<UserBanModel> _logger;

        public UserBanModel(ILogger<UserBanModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}
