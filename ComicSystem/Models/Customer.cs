namespace ComicSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
