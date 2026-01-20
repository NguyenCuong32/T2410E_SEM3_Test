using System.ComponentModel.DataAnnotations;

namespace ComicSystem.DTOs
{
    public class RentalReportDto
    {
        public int No { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class RentalReportFilterDto
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
