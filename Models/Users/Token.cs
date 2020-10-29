using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_gallery.Models.Users
{
    public class Token : Model
    {
        [BsonRequired]
        public string Value { get; set; } = null!;
    }
}
