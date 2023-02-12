using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecordsDTOs.CitizensDTOs
{
    public class CitizenDTO
    {
        
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string SocialSecurityNumber { get; set; }

        [MaxLength(100)]
        [Required]
        public string? PassportNumber { get; set; }
    }
}
