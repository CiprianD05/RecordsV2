
using AutoMapper;
using RecordsModels;
using RecordsDTOs.DocumentDTOs;

namespace RecordsDTOs.Profiles.DocumentsProfiles
{
    public class DocumentsAutoMappingProfile : Profile
    {
        public DocumentsAutoMappingProfile()
        {
            CreateMap<Document, DocumentReadDTO>();
            CreateMap<DocumentCreateDTO, Document>();
            CreateMap<DocumentUpdateDTO, Document>();

        }



    }
}
