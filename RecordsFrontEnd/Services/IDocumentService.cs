using RecordsDTOs.CitizensDTOs;
using RecordsDTOs.DocumentDTOs;

namespace RecordsFrontEnd.Services
{
    public interface IDocumentService
    {
        List<DocumentReadDTO> Documents { get; set; }

        Task GetDocuments(int Id);

        Task<HttpResponseMessage> GetDocument(int id);
        Task<HttpResponseMessage> DeleteDocument(int id);

        Task<HttpResponseMessage> CreateDocument(int CitizenId, int DocumentTypeId, MultipartFormDataContent documentCreateDTO);

        Task<HttpResponseMessage> UpdateDocument(int documentId, MultipartFormDataContent documentUpdateDTO);
        Task<byte[]> DownloadPdf(int id);

    }
}
