using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using web_gallery.Services;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class AlbumsModel : PageModel
    {
        private readonly AlbumService _albumService;
        private readonly ILogger<AlbumsModel> _logger;

        [BindProperty]
        public List<Models.Media.Album> Albums { get; set; } = new List<Models.Media.Album> { };

        public AlbumsModel(ILogger<AlbumsModel> logger, AlbumService albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }

        public void OnGet()
        {
            Albums = _albumService.Get();
        }

        public IActionResult OnGetLoadedAlbums(int pageIndex, int pageSize)
        {
            Albums = _albumService.Get();
            var fromIdx = pageIndex * pageSize;
            var count = pageSize;
            List<Models.Media.Album>? albums = null;
            if (fromIdx > Albums.Count)
            {
                albums = Albums.GetRange(0, 0);
            } else
            if (fromIdx + count > Albums.Count)
            {
                albums = Albums.GetRange(fromIdx, Albums.Count - fromIdx);
            } else
            {
                albums = Albums.GetRange(fromIdx, count);
            }
            return Partial("_AlbumCardsPartial", albums);
        }
    }
}
