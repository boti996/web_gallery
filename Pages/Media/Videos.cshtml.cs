using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using web_gallery.Services;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class VideosModel : PageModel
    {
        private readonly VideoService _videoService;
        private readonly ILogger<VideosModel> _logger;

        [BindProperty]
        public List<Models.Media.Video> Videos { get; set; } = new List<Models.Media.Video>{};

        public VideosModel(ILogger<VideosModel> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        public void OnGet()
        {
            Videos = _videoService.Get();
        }
    }
}
