using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using AspNetCore.Identity.Mongo.Model;
using System;

namespace web_gallery.Models.Users
{
    public class User : MongoUser, Model<ObjectId>
    {
        [BsonRequired]
        public string Mail { get; set; } = null!;
        [BsonRequired]
        public Details Details { get; set; } = null!;
        [BsonRequired]
        public string Salt { get; set; } = null!;
        [BsonRequired]
        public string Password { get; set; } = null!;
        [BsonRequired]
        public Permissions Permissions { get; set; } = null!;
        public Suspended? Suspended { get; set; } = null;

        public uint checkSuspended()
        {
            if (Suspended?.IsSuspended is true)
            {
                if (Suspended!.Until.Equals(DateTime.MaxValue))
                {
                    return uint.MaxValue;
                }
                var daysLeft = (Suspended!.Until - DateTime.Now).Days;
                return (
                    daysLeft > 0
                    ? Convert.ToUInt32(daysLeft)
                    : 0
                );
            }
            return 0;
        }

        public void setSuspended(bool IsSuspended, bool SuspendOnly)
        {
            Suspended = new Suspended
            {
                IsSuspended = IsSuspended,
                Until = Preferences.getSuspendDate(SuspendOnly)
            };
        }
    }

    public class Suspended
    {
        public bool IsSuspended { get; set; } = false;
        public DateTime Until { get; set; } = DateTime.MinValue;
    }

    public class Details
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class Permissions
    {
        public string User_type { get; set; } = UserType.Editor;
    }

    public struct UserType
    {
        public static string Editor = "editor";
        public static string Admin = "admin";
    }
}
