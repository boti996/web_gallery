using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace web_gallery.Pages
{
    [Authorize(Policy = PolicyNames.RequireAdminRole)]
    public class MediaModerateModel : PageModel
    {
        private readonly ILogger<MediaModerateModel> _logger;

        public MediaModerateModel(ILogger<MediaModerateModel> logger)
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
