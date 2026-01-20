using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicRentalApp.Models
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
