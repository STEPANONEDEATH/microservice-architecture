using ExampleCore.HttpLogic;
using ProfileConnectionLib.Models; // если есть модели пользователя
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ProfileConnectionLib
{
    // Добавляем только новый DTO
    public class CheckUserExistsResponse
    {
        public bool Exists { get; set; }
    }

    public class ProfileServiceClient : IProfileServiceClient
    {
        private readonly IHttpRequestService _httpService;
        private readonly string _baseUrl;

        public ProfileServiceClient(IHttpRequestService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _baseUrl = configuration["Services:ProfileServiceUrl"] ?? "http://localhost:5014";
        }

        public async Task<UserProfileDto?> GetUserAsync(Guid userId)
        {
            var url = $"{_baseUrl}/api/profiles/{userId}";
            return await _httpService.GetAsync<UserProfileDto>(url);
        }

        // Новый метод проверки существования пользователя
        public async Task<CheckUserExistsResponse?> UserExistsAsync(Guid userId)
        {
            var url = $"{_baseUrl}/api/profiles/{userId}/exists";
            return await _httpService.GetAsync<CheckUserExistsResponse>(url);
        }
    }
}