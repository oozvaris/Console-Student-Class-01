using Microsoft.AspNetCore.Mvc;
using SchoolApp_MVC.ApiClients.Interfaces;
using SchoolApp_MVC.Dtos.Students;

namespace SchoolApp_MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentApiClient _studentApiClient;
        public StudentsController(IStudentApiClient studentApiClient)
        {
            _studentApiClient = studentApiClient;

         }

        //public IActionResult Index()
        //{
        //    var students = _studentService.DisplayStudentListAsync().GetAwaiter().GetResult();

        //    return View(students);
        //}

        public async Task<IActionResult> Index()
        {
            var students = await _studentApiClient.GetAllAsync();

            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentApiClient.FindStudentByIdAsync(id);
            if (student == null)
            {
                //return NotFound();

                return View("NotFound");
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentApiClient.FindStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentUpdateDto = new StudentUpdateDto
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                StudentEmail = student.StudentEmail
            };

            return View(studentUpdateDto);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentUpdateDto studentUpdateDto)
        {
            if (id != studentUpdateDto.StudentID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(studentUpdateDto);
            }
            var studentToUpdate = new StudentUpdateDto
            {
                StudentID = studentUpdateDto.StudentID,
                StudentName = studentUpdateDto.StudentName,
                StudentSurname = studentUpdateDto.StudentSurname,
                StudentEmail = studentUpdateDto.StudentEmail
            };

            var result = await _studentApiClient.UpdateAsync(studentToUpdate.StudentID, studentToUpdate);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Update operation failed.");
                return View(studentUpdateDto);
            }

            TempData["SuccessMessage"] = "Student updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var student = await _studentService.FindStudentByIdAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    await _studentService.DeleteStudentAsync(id);

        //    TempData["SuccessMessage"] = "Student deleted successfully.";
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult Create()
        //{
        //    return View(new StudentCreateDto());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(StudentCreateDto studentCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(studentCreateDto);
        //    }
        //    var studentToCreate = new Student
        //    {
        //        // StudentID = 0,
        //        StudentName = studentCreateDto.StudentName,
        //        StudentSurname = studentCreateDto.StudentSurname,
        //        StudentEmail = studentCreateDto.StudentEmail
        //    };

        //    var result = await _studentService.AddStudentAsync(studentToCreate);

        //    if (!result)
        //    {
        //        ModelState.AddModelError(string.Empty, "Create operation failed.");
        //        return View(studentCreateDto);
        //    }

        //    TempData["SuccessMessage"] = "Student crated successfully.";
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult StudentsList(int id)
        //{
        //    ViewData["id"] = id;
        //    return View();
        //}
    }
}
