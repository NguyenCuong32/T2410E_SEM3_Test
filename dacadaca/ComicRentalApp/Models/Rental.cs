using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicRentalApp.Models
{
    public class Rental
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
