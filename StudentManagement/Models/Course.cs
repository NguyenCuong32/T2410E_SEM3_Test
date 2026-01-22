using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentManagement.Models
{
    public class Course
    {
        
        [Key]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Tên khóa học là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên khóa học không được vượt quá 200 ký tự")]
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số tín chỉ là bắt buộc")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải từ 1 đến 10")]
        public int Credits { get; set; }

        [MaxLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        public string? Department { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}