using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecordsModels;

namespace RecordsDTOs.Profiles.CitizensProfiles
{
    public class CitizenAutoMappingProfile:Profile
    {
        public CitizenAutoMappingProfile()
        {
            CreateMap<Citizen, CitizensDTOs.CitizenReadDTO>();
            CreateMap<Task<Citizen>, CitizensDTOs.CitizenReadDTO>();
            CreateMap<CitizensDTOs.CitizenCreateDTO, Citizen>();
            CreateMap<CitizensDTOs.CitizenUpdateDTO, Citizen>();
            CreateMap<CitizensDTOs.CitizenUpdateDTO, Task<Citizen>>();
        }
    }
}
