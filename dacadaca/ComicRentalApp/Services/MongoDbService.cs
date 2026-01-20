using MongoDB.Bson;
using MongoDB.Driver;
using ComicRentalApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicRentalApp.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<ComicBook> _comicBooks;
        private readonly IMongoCollection<Customer> _customers;
        private readonly IMongoCollection<Rental> _rentals;
        private readonly IMongoCollection<RentalDetail> _rentalDetails;

        // Constructor
        public MongoDbService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDB:ConnectionString").Value;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ComicSystem");

            _comicBooks = database.GetCollection<ComicBook>("ComicBooks"); // ComicBooks collection
            _customers = database.GetCollection<Customer>("Customers"); // Customers collection
            _rentals = database.GetCollection<Rental>("Rentals"); // Rentals collection
            _rentalDetails = database.GetCollection<RentalDetail>("RentalDetails"); // RentalDetails collection
        }

        // Get all comic books
        public async Task<List<ComicBook>> GetComicBooks()
        {
            return await _comicBooks.Find(cb => true).ToListAsync(); // Fetch all comic books from the database
        }

        // Get a single comic book by ID
        public async Task<ComicBook> GetComicBookById(string id)
        {
            var filter = Builders<ComicBook>.Filter.Eq(cb => cb.Id, ObjectId.Parse(id)); // Filter by comic book ID
            return await _comicBooks.Find(filter).FirstOrDefaultAsync(); // Fetch the comic book by ID
        }

        // Create a new comic book
        public async Task CreateComicBook(ComicBook comicBook)
        {
            await _comicBooks.InsertOneAsync(comicBook); // Insert a new comic book into the database
        }

        // Update an existing comic book
        public async Task UpdateComicBook(ComicBook updatedComicBook)
        {
            var filter = Builders<ComicBook>.Filter.Eq(cb => cb.Id, updatedComicBook.Id); // Find the comic book by ID
            await _comicBooks.ReplaceOneAsync(filter, updatedComicBook); // Replace the old comic book with the updated one
        }

        // Delete a comic book by ID
        public async Task DeleteComicBook(string id)
        {
            var filter = Builders<ComicBook>.Filter.Eq(cb => cb.Id, ObjectId.Parse(id)); // Filter by comic book ID
            await _comicBooks.DeleteOneAsync(filter); // Delete the comic book from the database
        }

        // Create a new customer
        public async Task CreateCustomer(Customer customer)
        {
            await _customers.InsertOneAsync(customer); // Insert a new customer into the database
        }

        // Create a new rental
        public async Task CreateRental(Rental rental)
        {
            await _rentals.InsertOneAsync(rental); // Insert a new rental into the database
        }

        // Create rental details
        public async Task CreateRentalDetail(RentalDetail rentalDetail)
        {
            await _rentalDetails.InsertOneAsync(rentalDetail); // Insert rental detail into the database
        }

        // Get a single customer by ID
        public async Task<Customer> GetCustomerById(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
            return await _customers.Find(filter).FirstOrDefaultAsync(); // Fetch customer by ID
        }

        // Get a single rental by ID
        public async Task<Rental> GetRentalById(string id)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.Id, ObjectId.Parse(id));
            return await _rentals.Find(filter).FirstOrDefaultAsync(); // Fetch rental by ID
        }

        // Get rental details by rental ID
        public async Task<List<RentalDetail>> GetRentalDetailsByRentalId(string rentalId)
        {
            var filter = Builders<RentalDetail>.Filter.Eq(rd => rd.RentalId, ObjectId.Parse(rentalId));
            return await _rentalDetails.Find(filter).ToListAsync(); // Fetch rental details by rental ID
        }
    }
}
