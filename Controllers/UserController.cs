using CTF_Platform_dotnet.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CTF_Platform_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser(int  id)
        {
            var user = new
            {
                Id = id,
                Name = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            };
            return Ok(user);
        }

        //[Authorize(Roles = nameof(RoleEnum.Admin))]
        //[HttpGet("getadmin")]
        //public IActionResult GetAdmin()
        //{
        //    return Ok(new { Message = "This is an admin-only endpoint" });
        //}

        //[Authorize(Roles = nameof(RoleEnum.Participant))]
        //[HttpGet("getuser")]
        //public IActionResult GetParticipant()
        //{
        //    return Ok(new { Message = "This is a participant-only endpoint" });
        //}
    }
}
