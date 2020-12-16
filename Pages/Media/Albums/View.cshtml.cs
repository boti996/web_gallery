using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using web_gallery.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class AlbumViewModel : PageModel
    {
        [BindProperty]
        public List<string> Images { get; set; } = new List<string>();
        private readonly AlbumService _albumService;
        private readonly ImageBlobService _imageBlobService;
        private readonly ILogger<AlbumViewModel> _logger;

        public AlbumViewModel(
            ILogger<AlbumViewModel> logger,
            ImageBlobService imageBlobService,
            AlbumService albumService
        )
        {
            _logger = logger;
            _imageBlobService = imageBlobService;
            _albumService = albumService;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var album = _albumService.Get(id);
            
            if (album == null)
            {
                return RedirectToPage("/Warning", new
                {
                    warningMessage = WarningMessages.InvalidValueSelected,
                    returnUrl = "/Media/Albums"
                });
            }

            await foreach (var item in _imageBlobService.GetBlobCatalog(album.Id))
            {
                if (!item.IsPrefix)
                {
                    Images.Add(_imageBlobService.getCdnUrl(item.Blob.Name));
                }
            }

            return Page();
        }
    }
}