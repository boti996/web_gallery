namespace web_gallery.Models
{
    public enum UserTypes
    {
        User = 0,
        Editor = 1,
        Admin = 2
    }
    public interface IUserManagement
    {
        Models.UserTypes UserType { get; }
        bool userHasAccess(Models.ContentAccessProperties pageProperties);
    }
}
