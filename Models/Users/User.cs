using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace web_gallery.Models.Users
{
    public class User : Model
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
