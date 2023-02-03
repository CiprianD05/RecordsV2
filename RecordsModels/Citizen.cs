using System.ComponentModel.DataAnnotations;
namespace RecordsModels
{
    public class Citizen
    {
        [Key]
        [Required]        
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string SocialSecurityNumber { get; set; }

        [MaxLength(100)]
        public string? PassportNumber { get; set; }

    }
}