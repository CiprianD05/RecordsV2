using RecordsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.PsychologicalProfileDTOs
{
    public class PsychologicalProfileCreateDTO
    {

        

        [Required]
        public int CitizenId { get; set; }

        [Required]
        public string Psychologist { get; set; }

        [Required]
        public string Felony { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string PersonalAndFamilyHistory { get; set; }

        [Required]
        public string PersonalityTraits { get; set; }

        [Required]
        public string ImpulsiveBehaviorAndAngertManagementProblems { get; set; }

        public Citizen Citizen { get; set; }

    }
}
