using Microsoft.AspNetCore.Mvc;
using ProfileService.Api.Dto;
using System.Collections.Concurrent;

namespace ProfileService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        // Простое in-memory хранилище (для домашней работы)
        private static readonly ConcurrentDictionary<string, string> _store = new();

        [HttpPost("register")]
        public ActionResult<RegisterProfileResponse> Register([FromBody] RegisterProfileRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserId))
                return BadRequest(new RegisterProfileResponse(false));

            _store[req.UserId] = req.FullName ?? string.Empty;
            return Ok(new RegisterProfileResponse(true));
        }

        [HttpGet("{userId}/exists")]
        public ActionResult<CheckUserExistsResponse> Exists(string userId)
        {
            var exists = _store.ContainsKey(userId);
            return Ok(new CheckUserExistsResponse(exists));
        }
    }
}