using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI_DotNet7.Models.DTO
{
    public class SuperHeroDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Place { get; set; } 
    }
}
