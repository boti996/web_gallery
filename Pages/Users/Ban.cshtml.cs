using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using web_gallery.Services;
using System.Collections.Generic;
using MongoDB.Bson;
using System;

namespace web_gallery.Pages
{
    [Authorize(Policy = PolicyNames.RequireAdminRole)]
    public class UserBanModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Select user")]
        [Required]
        public string UserSelect { get; set; } = "";

        public ObjectId getDefaultOBjectId()
        {
            return ObjectId.Empty;
        }

        [BindProperty]
        [Required]
        public bool SuspendOnly { set; get; } = false;

        private readonly UserService _userService;
        private readonly ILogger<UserBanModel> _logger;

        [BindProperty]
        public List<Models.Users.User> Users { get; set; } = new List<Models.Users.User> { };

        public UserBanModel(
            ILogger<UserBanModel> logger,
            UserService userService
        )
        {
            _logger = logger;
            _userService = userService;
        }

        public void OnGet()
        {
            Users = _userService.Get();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = new ObjectId(UserSelect);
            var userToBan = _userService.Get(userId);

            if (userToBan == null)
            {
                return RedirectToPage("/Warning", new
                {
                    warningMessage = WarningMessages.InvalidValueSelected,
                    returnUrl = "/Users/Ban"
                });
            }

            userToBan.setSuspended(true, SuspendOnly);
            _userService.Update(userId, userToBan);

            return RedirectToPage("/Warning", new { warningMessage = WarningMessages.UserWasBannedSuccessfully });
        }
    }
}
