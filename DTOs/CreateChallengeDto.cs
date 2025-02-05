using System.ComponentModel.DataAnnotations;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class CreateChallengeDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(CategoryEnum))]
        public CategoryEnum Category { get; set; }

        [Required]
        [EnumDataType(typeof(DifficultyEnum))]
        public DifficultyEnum Difficulty { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Points { get; set; }

        [Required]
        [StringLength(255)]
        public string Flag { get; set; }

        [StringLength(255)]
        public string? FilePath { get; set; }
    }
}