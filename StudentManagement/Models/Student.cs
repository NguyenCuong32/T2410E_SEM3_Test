using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization; 

namespace StudentManagement.Models
{
    public class Student
    {
       
        [Key]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Tên sinh viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên không được vượt quá 200 ký tự")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required(ErrorMessage = "Ngày nhập học là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}