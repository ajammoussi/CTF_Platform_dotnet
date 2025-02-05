using System.ComponentModel.DataAnnotations;
using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class UpdateChallengeDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [EnumDataType(typeof(CategoryEnum))]
        public CategoryEnum? Category { get; set; }

        [EnumDataType(typeof(DifficultyEnum))]
        public DifficultyEnum? Difficulty { get; set; }

        [Range(1, 10000)]
        public int? Points { get; set; }

        [StringLength(255)]
        public string? Flag { get; set; }

        [StringLength(255)]
        public string? FilePath { get; set; }
    }
}