using RecordsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.PsychologicalProfileDTOs
{
    public class PsychologicalProfileReadDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CitizenId { get; set; }

        [Required]
        public string Psychologist { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

      

        [Required]
        public string Summary { get; set; }

       

        public Citizen Citizen { get; set; }
    }
}
