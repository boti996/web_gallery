namespace web_gallery.Models
{
    public class MockLogin : ISessionManagement
    {
        public string? UserId { get; protected set; }
        private bool loggedIn = false;
        public bool isLoggedIn() { return this.loggedIn; }
        public void login(string userId="testuser", string password="testpassword") 
        {
            this.loggedIn = true;
            this.UserId = userId;
        }
        public void logout() { this.loggedIn = false; }
        public bool userHasAccess(Models.ContentAccessProperties pageProperties) { return true; }
    }
}
