using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_gallery.Models
{
    public class BasePageModel : PageModel
    {
        public ISessionManagement session { get; set; } = new Models.MockLogin();
        public IUserManagement user { get; seŧ; }
    }
}
