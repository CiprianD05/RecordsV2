using AutoMapper;
using RecordsModels;
using RecordsDTOs.PsychologicalProfileDTOs;
using RecordsDTOs.DocumentDTOs;

namespace RecordsDTOs.Profiles.PsychologicalProfileProfiles
{
    public class PsychologicalProfileAutomappingProfile:Profile
    {
        public PsychologicalProfileAutomappingProfile()
        {
            CreateMap<PsychologicalProfile, PsychologicalProfileReadDTO>();
            CreateMap<PsychologicalProfileCreateDTO, PsychologicalProfile>();
            CreateMap<PsychologicalProfileUpdateDTO, PsychologicalProfile>();
        }

    }
}
