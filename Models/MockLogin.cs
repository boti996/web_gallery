namespace web_gallery.Models
{
    public class MockLogin : ISessionManagement, IUserManagement
    {
        public string? UserId { get; protected set; } = null;
        public Models.UserTypes UserType { get; protected set; } = Models.UserTypes.User;

        private bool loggedIn = false;
        public bool isLoggedIn() { return this.loggedIn; }
        public void login(string userId="testuser", string password="testpassword") 
        {
            this.loggedIn = true;
            this.UserId = userId;
        }
        public void logout() { this.loggedIn = false; }

        bool ISessionManagement.userHasAccess(Models.ContentAccessProperties pageProperties)
        {
            if (pageProperties.IsLoginRequired)
            {
                return this.isLoggedIn();
            }
            return true;
        }

        bool IUserManagement.userHasAccess(ContentAccessProperties pageProperties)
        {
            return pageProperties.MinPrivilegeLevel <= this.UserType;
        }
    }
}
