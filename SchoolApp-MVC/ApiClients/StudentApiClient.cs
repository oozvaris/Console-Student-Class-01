using SchoolApp_MVC.ApiClients.Interfaces;
using SchoolApp_MVC.Dtos.Students;

namespace SchoolApp_MVC.ApiClients
{
    public class StudentApiClient : IStudentApiClient
    {
        private readonly HttpClient _httpClient;

        public StudentApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<StudentReadDto>> GetAllAsync()
        {
            var students = await _httpClient.GetFromJsonAsync<IReadOnlyList<StudentReadDto>>("api/students");
            return students ?? Array.Empty<StudentReadDto>();
        }

    }
}
