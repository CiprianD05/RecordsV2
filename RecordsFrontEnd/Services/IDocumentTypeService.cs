using RecordsDTOs.DocumentTypeDTOs;

namespace RecordsFrontEnd.Services
{
    public interface IDocumentTypeService
    {

        List<DocumentTypeReadDTO> DocumentTypes { get; set; }

        Task GetDocumentTypes();

        Task<HttpResponseMessage> GetDocumentType(int id);
        Task<HttpResponseMessage> DeleteDocumentType(int id);

        Task<HttpResponseMessage> CreateDocumentType(DocumentTypeCreateDTO DocumentTypeCreateDTO);

        Task<HttpResponseMessage> UpdateDocumentType(int id, DocumentTypeUpdateDTO DocumentTypeUpdateDTO);


    }
}
