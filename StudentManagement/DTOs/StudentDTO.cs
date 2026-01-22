namespace StudentManagement.DTOs
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<EnrolledCourseDTO> Courses { get; set; } = new List<EnrolledCourseDTO>();
    }

    public class EnrolledCourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string? Department { get; set; }
        public decimal? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
