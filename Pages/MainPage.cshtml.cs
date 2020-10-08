using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace web_gallery.Pages
{
    public class MainPageModel : Models.BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public MainPageModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; private set; } = "PageModel in C#";

        public void OnGet()
        {
            Message += $" Server time is { DateTime.Now }";
        }
    }
}
