
using AutoMapper;
using RecordsModels;

namespace RecordsDTOs.Profiles.CitizensProfiles
{
    public class CitizenAutoMappingProfile:Profile
    {
        public CitizenAutoMappingProfile()
        {
            CreateMap<Citizen, CitizensDTOs.CitizenReadDTO>();
            //CreateMap<Task<Citizen>, CitizensDTOs.CitizenReadDTO>();
            CreateMap<CitizensDTOs.CitizenCreateDTO, Citizen>();
            CreateMap<CitizensDTOs.CitizenUpdateDTO, Citizen>();
            //CreateMap<CitizensDTOs.CitizenUpdateDTO, Task<Citizen>>();
            CreateMap<CitizensDTOs.CitizenReadDTO, CitizensDTOs.CitizenUpdateDTO>();

        }
    }
}
