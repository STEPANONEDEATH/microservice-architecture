using ExampleCore.HttpLogic;
using ProfileConnectionLib.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ProfileConnectionLib
{
    public class ProfileServiceClient : IProfileServiceClient
    {
        private readonly IHttpRequestService _httpService;
        private readonly string _baseUrl;

        public ProfileServiceClient(IHttpRequestService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _baseUrl = configuration["Services:ProfileServiceUrl"] ?? "http://localhost:5001";
        }

        public async Task<UserProfileDto?> GetUserAsync(Guid userId)
        {
            var url = $"{_baseUrl}/api/profiles/{userId}";
            return await _httpService.GetAsync<UserProfileDto>(url);
        }
    }
}