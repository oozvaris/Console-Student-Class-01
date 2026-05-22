using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp_Backend.Dtos.Students;
using SchoolApp_Backend.Services;

namespace SchoolApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StudentReadDto>>> GetAllStudents()
        {
            var students = await _studentService.DisplayStudentListAsync();
            if (students == null || !students.Any())
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("{studentId:int}")]
        public async Task<ActionResult<StudentReadDto>> GetStudentById(int studentId)
        {
            var student = await _studentService.FindStudentByIdAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);

        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent(StudentCreateDto studentCreateDto)
        {
            var result = await _studentService.AddStudentAsync(studentCreateDto);
            if (result == null)
            {
                return BadRequest();
            }
            return NoContent();
        }


        [HttpPut("{studentId:int}")]
        public async Task<ActionResult> UpdateStudent(int studentId, StudentUpdateDto studentUpdateDto)
        {
            var student = await _studentService.FindStudentByIdAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }

            var result = await _studentService.UpdateStudentAsync(studentUpdateDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{studentId:int}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var student = await _studentService.FindStudentByIdAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }
            await _studentService.DeleteStudentAsync(studentId);
            return NoContent();

        }
    }


}
