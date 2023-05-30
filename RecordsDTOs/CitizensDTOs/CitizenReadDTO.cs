using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecordsDTOs.CitizensDTOs
{
    public class CitizenReadDTO
    {
        
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is requeired")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is requeired")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Social Security Number is requeired")]
        [MaxLength(100)]
        public string SocialSecurityNumber { get; set; }

        [MaxLength(100)]
        
        public string? PassportNumber { get; set; }
    }
}
