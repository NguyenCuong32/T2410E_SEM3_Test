using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicRentalApp.Models
{
    public class ComicBook
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
