using CTF_Platform_dotnet.Data;
using Microsoft.AspNetCore.Mvc;

namespace CTF_Platform_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly CTFContext _context;

        public SeedController(CTFContext context)
        {
            _context = context;
        }

        [HttpPost("seed-database")] // POST /api/seed/seed-database
        public IActionResult SeedDatabase()
        {
            DatabaseSeeder.Seed(_context);
            return Ok("Database seeded successfully!");
        }

        [HttpPost("reset-database")] // POST /api/seed/reset-database
        public IActionResult ResetDatabase()
        {
            _context.Database.EnsureDeleted(); // Delete the database
            _context.Database.EnsureCreated(); // Recreate the database
            DatabaseSeeder.Seed(_context); // Seed with mock data
            return Ok("Database reset and seeded successfully!");
        }
    }
}
