using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicRentalApp.Models
{
    public class RentalDetail
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId RentalId { get; set; }
        public ObjectId ComicBookId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
