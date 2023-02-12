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
            CreateMap<Citizen, CitizensDTOs.CitizenDTO>();
        }
    }
}
