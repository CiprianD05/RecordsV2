
using RecordsDTOs.CitizensDTOs;
using RecordsDTOs.DocumentTypeDTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace RecordsFrontEnd.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly HttpClient _httpClient;

        public DocumentTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<DocumentTypeReadDTO> DocumentTypes { get ; set; }=new List<DocumentTypeReadDTO> ();



        public async Task<HttpResponseMessage> CreateDocumentType(DocumentTypeCreateDTO DocumentTypeCreateDTO)
        {
            var result = await _httpClient.PostAsJsonAsync($"https://localhost:7190/api/documenttype/", DocumentTypeCreateDTO);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteDocumentType(int id)
        {
            var result = await _httpClient.DeleteAsync($"https://localhost:7190/api/documenttype/{id}");

            return result;
        }

        public async Task<HttpResponseMessage> GetDocumentType(int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7190/api/documenttype/{id}");

            return result;
        }

        public async Task GetDocumentTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<DocumentTypeReadDTO>>("https://localhost:7190/api/documenttype");

            if (result != null)
                DocumentTypes = result;
        
        }

        public async Task<HttpResponseMessage> UpdateDocumentType(int id, DocumentTypeUpdateDTO DocumentTypeUpdateDTO)
        {
            var result = await _httpClient.PutAsJsonAsync($"https://localhost:7190/api/documenttype/{id}", DocumentTypeUpdateDTO);

            return result;
        }
    }
}
