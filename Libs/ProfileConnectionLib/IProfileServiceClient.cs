using ProfileConnectionLib.Models;
using System;
using System.Threading.Tasks;

namespace ProfileConnectionLib
{
    public interface IProfileServiceClient
    {
        Task<UserProfileDto?> GetUserAsync(Guid userId);
    }
}