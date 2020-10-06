namespace web_gallery.Models
{
    public interface ISessionManagement
    {
        string? UserId { get; }
        bool isLoggedIn();
        void login(string userId, string password);
        void logout(); 
        bool userHasAccess(Models.ContentAccessProperties pageProperties);
    }
}
