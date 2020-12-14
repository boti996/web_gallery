using System.Collections.Generic;
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
        public List<Models.Media.Video> Videos { get; set; } = new List<Models.Media.Video> { };

        public VideosModel(ILogger<VideosModel> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        public void OnGet()
        {
            Videos = _videoService.Get();
        }

        public IActionResult OnGetLoadedVideos(int pageIndex, int pageSize)
        {
            Videos = _videoService.Get();
            var fromIdx = pageIndex * pageSize;
            var count = pageSize;
            List<Models.Media.Video>? videos = null;
            if (fromIdx > Videos.Count)
            {
                videos = Videos.GetRange(0, 0);
            } else
            if (fromIdx + count > Videos.Count)
            {
                videos = Videos.GetRange(fromIdx, Videos.Count - fromIdx);
            } else
            {
                videos = Videos.GetRange(fromIdx, count);
            }
            return Partial("_VideoCardsPartial", videos);
        }
    }
}
