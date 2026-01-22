using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DBContext;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly StudentManagementDBContext _context;

        public EnrollmentController(StudentManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        // GET: api/Enrollment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound(new { message = $"Không tìm thấy bản ghi đăng ký với ID {id}" });
            }

            return enrollment;
        }

        // POST: api/Enrollment
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate StudentID exists
            var studentExists = await _context.Students.AnyAsync(s => s.StudentID == enrollment.StudentID);
            if (!studentExists)
            {
                return BadRequest(new { message = $"Không tìm thấy sinh viên với ID {enrollment.StudentID}" });
            }

            // Validate CourseID exists
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseID == enrollment.CourseID);
            if (!courseExists)
            {
                return BadRequest(new { message = $"Không tìm thấy khóa học với ID {enrollment.CourseID}" });
            }

            // Check if enrollment already exists
            var enrollmentExists = await _context.Enrollments
                .AnyAsync(e => e.StudentID == enrollment.StudentID && e.CourseID == enrollment.CourseID);
            if (enrollmentExists)
            {
                return BadRequest(new { message = "Sinh viên đã đăng ký khóa học này rồi" });
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.EnrollmentID }, enrollment);
        }

        // PUT: api/Enrollment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return BadRequest(new { message = "ID không khớp" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate StudentID exists
            var studentExists = await _context.Students.AnyAsync(s => s.StudentID == enrollment.StudentID);
            if (!studentExists)
            {
                return BadRequest(new { message = $"Không tìm thấy sinh viên với ID {enrollment.StudentID}" });
            }

            // Validate CourseID exists
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseID == enrollment.CourseID);
            if (!courseExists)
            {
                return BadRequest(new { message = $"Không tìm thấy khóa học với ID {enrollment.CourseID}" });
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy bản ghi đăng ký với ID {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Enrollment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound(new { message = $"Không tìm thấy bản ghi đăng ký với ID {id}" });
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}
