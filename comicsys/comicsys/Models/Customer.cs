using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
	public class Customer
	{
		public int CustomerID { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		public DateTime RegistrationDate { get; set; }
	}
}