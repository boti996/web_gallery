using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace web_gallery.Pages
{
    public class UserEditModel : PageModel
    {
        private readonly ILogger<UserEditModel> _logger;

        public UserEditModel(ILogger<UserEditModel> logger)
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
