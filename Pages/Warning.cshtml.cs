using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace web_gallery.Pages
{
    [AllowAnonymous]
    public class WarningModel : PageModel
    {
        private readonly ILogger<WarningModel> _logger;

        public WarningModel(ILogger<WarningModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
