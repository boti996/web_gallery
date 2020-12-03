using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_gallery.Models.Media
{
    public class Video : Model<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonRequired]
        public Details Details { get; set; } = null!;
        [BsonRequired]
        public string Resource_link { get; set; } = null!;
        [BsonRequired]
        public bool Is_public { get; set; } = true;
    }


}
