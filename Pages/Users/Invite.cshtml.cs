using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace web_gallery.Pages
{
    [Authorize(Policy = PolicyNames.RequireAdminRole)]
    public class UserInviteModel : PageModel
    {
        private readonly ILogger<UserInviteModel> _logger;

        public UserInviteModel(ILogger<UserInviteModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}
