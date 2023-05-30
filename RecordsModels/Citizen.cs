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
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string SocialSecurityNumber { get; set; }

        [MaxLength(100)]    
        public string? PassportNumber { get; set; }


       // public virtual ICollection<Document> Documents { get; set; }

    }
}