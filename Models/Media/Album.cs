using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_gallery.Models.Media
{
    public class Album : Model
    {
        [BsonRequired]
        public Details Details { get; set; } = null!;
        [BsonRequired]
        public bool Is_public { get; set; } = true;
        [BsonRequired]
        public string Resource_link { get; set; } = null!;
    }


}
