namespace ComicSystem.Models;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    public string PhoneNumber { get; set; }

    public DateTime RegistrationDate { get; set; }
}

