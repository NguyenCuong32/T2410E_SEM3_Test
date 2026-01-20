using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comic_Book_Rental.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        public int CustomerID { get; set; }

        // Tạo quan hệ khóa ngoại
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public DateTime RentalDate { get; set; } = DateTime.Now;

        public DateTime ReturnDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } // Ví dụ: "Dang thue", "Da tra"

        // Một lần thuê có thể có nhiều chi tiết
        public virtual ICollection<RentalDetail> RentalDetails { get; set; }
    }
}