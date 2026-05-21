using DAL.Models;
using SchoolApp_Backend.Dtos.Students;

namespace SchoolApp_Backend.Services
{
    public interface IStudentService
    {
        Task<IReadOnlyList<StudentReadDto>> DisplayStudentListAsync();
        Task<bool> AddStudentAsync(Student student);
        Task DeleteStudentAsync(int studentId);

        Task<bool> UpdateStudentAsync(Student student);

        Task<StudentReadDto?> FindStudentByIdAsync(int studentId);
    }
}
