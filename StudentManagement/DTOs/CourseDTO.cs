namespace StudentManagement.DTOs
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string? Department { get; set; }
        public List<EnrolledStudentDTO> Students { get; set; } = new List<EnrolledStudentDTO>();
    }

    public class EnrolledStudentDTO
    {
        public int StudentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
