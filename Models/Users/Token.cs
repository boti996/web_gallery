using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_gallery.Models.Users
{
    public class Token : Model<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonRequired]
        public string Value { get; set; } = null!;
        [BsonRequired]
        public bool IsValid { get; set; } = false!;
        [BsonIgnore]
        public DateTime Timestamp => new ObjectId(Id).CreationTime;
    }
}
