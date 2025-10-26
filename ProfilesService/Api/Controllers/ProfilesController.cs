using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Application.Dto; // <-- теперь DTO берутся из Application
using ProfilesService.Domain.Entities;
using ProfilesService.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace ProfilesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesDbContext _dbContext;

        public ProfilesController(ProfilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterProfileResponse>> Register(RegisterProfileRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserId))
                return BadRequest(new RegisterProfileResponse(false));

            var userId = Guid.Parse(req.UserId);

            var existingUser = await _dbContext.Users.FindAsync(userId);
            if (existingUser != null)
                return Conflict(new RegisterProfileResponse(false));

            _dbContext.Users.Add(new User
            {
                Id = userId,
                Username = req.FullName ?? string.Empty,
                Email = "",
                PasswordHash = ""
            });

            await _dbContext.SaveChangesAsync();
            return Ok(new RegisterProfileResponse(true));
        }

        [HttpGet("{userId}/exists")]
        public async Task<ActionResult<CheckUserExistsResponse>> Exists(string userId)
        {
            var guid = Guid.Parse(userId);
            var exists = await _dbContext.Users.AnyAsync(u => u.Id == guid);
            return Ok(new CheckUserExistsResponse(exists));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileDto>> GetUser(string userId)
        {
            var guid = Guid.Parse(userId);
            var user = await _dbContext.Users.FindAsync(guid);
            if (user == null)
                return NotFound();

            var dto = new UserProfileDto(user.Id.ToString(), user.Username);
            return Ok(dto);
        }
    }
}
