using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace StudentManagement.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        
        [Required(ErrorMessage = "Ngày đăng ký là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
        public decimal? Grade { get; set; }

        [Required(ErrorMessage = "StudentID là bắt buộc")]
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }  

        [Required(ErrorMessage = "CourseID là bắt buộc")]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }  
    }
}
