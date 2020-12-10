using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using web_gallery.Services;

namespace web_gallery.Pages
{
    public class MediaUploadModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Select album")]
        [Required]
        public string AlbumSelect { set; get; } = null!;

        [BindProperty]
        [Display(Name = "Select images")]
        [Required]
        public string ImageSelect { set; get; } = null!;
        private readonly AlbumService _albumService;
        private readonly ILogger<MediaUploadModel> _logger;

        [BindProperty]
        public List<Models.Media.Album> Albums { get; set; } = new List<Models.Media.Album>{};

        public MediaUploadModel(ILogger<MediaUploadModel> logger, AlbumService albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }

        public void OnGet()
        {
            Albums = _albumService.Get();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Video upload selector and redirect !!!
            // TODO: create or select album
            // TODO: upload images and add link to album

            return RedirectToPage("/Media/Albums/View", new { id=AlbumSelect });
        }
    }
}
