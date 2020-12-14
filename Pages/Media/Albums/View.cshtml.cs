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
        private readonly ImageBlobService _imageBlobService;
        private readonly ILogger<AlbumViewModel> _logger;

        public AlbumViewModel(
            ILogger<AlbumViewModel> logger,
            ImageBlobService imageBlobService
        )
        {
            _logger = logger;
            _imageBlobService = imageBlobService;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            // TODO: check id validity
            // TODO: get album detaiuls
            // TODO: get images from resource link
            await foreach (var item in _imageBlobService.GetBlobCatalog("pokemons"))
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