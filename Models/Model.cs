using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_gallery.Models
{
    public interface Model<TId>
    {
        public TId Id { get; set; }
    }
}
