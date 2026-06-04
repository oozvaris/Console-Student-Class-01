using SchoolApp_MVC.ApiClients.Interfaces;
using SchoolApp_MVC.Dtos.Students;
using System.Net;

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

        public async Task<StudentReadDto?> FindStudentByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/students/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            // var content = responseMessage.Content;

            var student = await responseMessage.Content.ReadFromJsonAsync<StudentReadDto>();
            return student ?? null;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateAsync(int id, StudentUpdateDto studentUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/students/{id}", studentUpdateDto);
            return await ToResultAsync(response);

        }

        private static async Task<(bool Success, string? ErrorMessage)> ToResultAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return (false, "Student not found.");
            }

            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            return (false, error?.ErrorMessage ?? "Student operation failed.");
        }

        private sealed class ApiError
        {
            public string? ErrorMessage { get; set; }
        }

    }
}
