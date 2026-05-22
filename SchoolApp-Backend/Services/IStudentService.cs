using DAL.Models;
using SchoolApp_Backend.Dtos.Students;

namespace SchoolApp_Backend.Services
{
    public interface IStudentService
    {
        Task<IReadOnlyList<StudentReadDto>> DisplayStudentListAsync();
        Task<bool> AddStudentAsync(StudentCreateDto studentCreateDto);
        Task DeleteStudentAsync(int studentId);

        Task<bool> UpdateStudentAsync(StudentUpdateDto studentUpdateDto);

        Task<StudentReadDto?> FindStudentByIdAsync(int studentId);
    }
}
