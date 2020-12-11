using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class AlbumViewModel : PageModel
    {
        private readonly ILogger<AlbumViewModel> _logger;

        public AlbumViewModel(ILogger<AlbumViewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string id)
        {
            // TODO: check id validity
            // TODO: get album detaiuls
            // TODO: get images from resource link
        }
    }
}
