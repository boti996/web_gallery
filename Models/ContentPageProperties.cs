namespace web_gallery.Models
{
    public class ContentAccessProperties
    {
        public Models.UserTypes MinPrivilegeLevel { get; protected set; }
        public bool IsLoginRequired { get; protected set; }
    } 
}
