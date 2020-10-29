namespace web_gallery.Models
{
    public class MediaDatabaseSettings : IMediaDatabaseSettings
    {
        public string AlbumCollectionName { get; set; } = null!;
        public string VideoCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

    public interface IMediaDatabaseSettings : IDatabaseSettings
    {
        string AlbumCollectionName { get; set; }
        string VideoCollectionName { get; set; }
    }
}