
using AutoMapper;
using RecordsModels;
using RecordsDTOs.DocumentTypeDTOs;

namespace RecordsDTOs.Profiles.DocumentTypesProfiles
{
    public class DocumentTypeAutoMappingProfile : Profile
    {
        public DocumentTypeAutoMappingProfile() 
        {
            CreateMap<DocumentType, DocumentTypeReadDTO>();
            CreateMap<DocumentTypeCreateDTO, DocumentType>();
            CreateMap<DocumentTypeUpdateDTO, DocumentType>();


        }
    }
}
