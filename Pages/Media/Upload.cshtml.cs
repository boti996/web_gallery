using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using web_gallery.Models.Media;
using web_gallery.Services;

namespace web_gallery.Pages
{
    public class MediaUploadModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Select album")]
        public string? AlbumSelect { set; get; } = null!;

        [BindProperty]
        [Display(Name = "Album name")]
        [Required]
        [MinLength(0)]
        [MaxLength(50)]
        public string AlbumName { set; get; } = null!;

        [BindProperty]
        [Display(Name = "Select images")]
        [Required]
        public IFormFile[] ImageSelect { set; get; } = null!;
        private IWebHostEnvironment _environment;
        private readonly AlbumService _albumService;
        private readonly ImageBlobService _imageBlobService;
        private readonly ILogger<MediaUploadModel> _logger;

        [BindProperty]
        public List<Models.Media.Album> Albums { get; set; } = new List<Models.Media.Album> { };

        public MediaUploadModel(
            IWebHostEnvironment hostEnvironment,
            ILogger<MediaUploadModel> logger, 
            ImageBlobService imageBlobService,    
            AlbumService albumService
        )
        {
            _environment = hostEnvironment;
            _logger = logger;
            _imageBlobService = imageBlobService;
            _albumService = albumService;
        }

        public void OnGet()
        {
            Albums = _albumService.Get();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var album = (
                AlbumSelect != null
                ? _albumService.Get(AlbumSelect)
                : new Album { Details = new Details{} }
            );
            album.Resource_link = album.Id;

            if (AlbumName.Length > 0)
            {
                album.Details.Name = AlbumName;
            }

            foreach (var image in ImageSelect)
            {
                var filePath = Path.Combine(_environment.ContentRootPath, image.FileName);
                var memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                _imageBlobService.CreateBlob(memoryStream, image.FileName, album.Id).Wait();
            }

            _albumService.Update(album.Id, album);

            return RedirectToPage("/Media/Albums/View", new { id = album.Id });
        }
    }
}
