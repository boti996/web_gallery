using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_gallery.Models
{
    public class BasePageModel : PageModel
    {
        public Models.ISessionManagement SessionManagement { get; set; }
        public Models.IUserManagement UserManagement { get; set; }
        public Models.ContentAccessProperties PageProperties { get; protected set; }
        public BasePageModel() {
            var mockLogin = new Models.MockLogin();
            this.SessionManagement = mockLogin;
            this.UserManagement = mockLogin;
            this.PageProperties = new ContentAccessProperties {
                MinPrivilegeLevel = Models.UserTypes.User,
                IsLoginRequired = false
            };
        }
        public bool userHasAccess() {
            return (
                this.SessionManagement.userHasAccess(this.PageProperties)
                && this.UserManagement.userHasAccess(this.PageProperties)
            );
        }
    }
}
