namespace web_gallery.Models
{
    public class UsersDatabaseSettings : IUsersDatabaseSettings
    {
        public string UserCollectionName { get; set; } = null!;
        public string TokenCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

    public interface IUsersDatabaseSettings : IDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string TokenCollectionName { get; set; }
    }
}
