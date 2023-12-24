using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI_DotNet7.Models.DTO
{
    public class UpdateSuperHero
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Place { get; set; } = string.Empty;
    }
}
