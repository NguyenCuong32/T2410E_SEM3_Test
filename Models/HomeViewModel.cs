using System.Collections.Generic;

namespace ASM.Models
{
    public class HomeViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<ComicBook> ComicBooks { get; set; }
        public List<Rental> Rentals { get; set; }
        public List<RentalDetail> RentalDetails { get; set; }

        public HomeViewModel()
        {
            Customers = new List<Customer>();
            ComicBooks = new List<ComicBook>();
            Rentals = new List<Rental>();
            RentalDetails = new List<RentalDetail>();
        }
    }
}
