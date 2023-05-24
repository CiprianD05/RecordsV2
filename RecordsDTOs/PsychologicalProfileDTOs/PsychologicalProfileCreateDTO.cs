using RecordsModels;
using System.ComponentModel.DataAnnotations;


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
