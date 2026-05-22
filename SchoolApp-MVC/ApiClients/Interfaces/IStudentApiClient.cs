using SchoolApp_MVC.Dtos.Students;

namespace SchoolApp_MVC.ApiClients.Interfaces
{
    public interface IStudentApiClient
    {
        Task<IReadOnlyList<StudentReadDto>> GetAllAsync();
        //Task<StudentReadDto> GetByIdAsync(int id);
        //Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(StudentCreateDto studentCreateDto);
        //Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(int id, StudentUpdateDto studentUpdateDto);
        //Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int id);
    }
}
