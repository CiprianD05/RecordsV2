using RecordsDTOs.CitizensDTOs;
using RecordsDTOs.DocumentDTOs;
using RecordsFrontEnd.Pages;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace RecordsFrontEnd.Services
{
    public class DocumentService : IDocumentService
    {
        public List<DocumentReadDTO> Documents { get ; set ; }=new List<DocumentReadDTO>();
        private readonly    HttpClient _httpClient;

        public DocumentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task GetDocuments(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<DocumentReadDTO>>($"https://localhost:7190/api/documents/citizenId/{id}");

            if (result != null)
                Documents = result;
        }

        public async Task<HttpResponseMessage> GetDocument(int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7190/api/documents/documentId/{id}");

            return result;
        }

        public async Task<HttpResponseMessage> CreateDocument(int CitizenId,int DocumentTypeId,MultipartFormDataContent files)
        {
            var result = await _httpClient.PostAsync($"https://localhost:7190/api/documents/{CitizenId}/{DocumentTypeId}", files);

            return result;
        }
        public async Task<HttpResponseMessage> UpdateDocument(int documentId, MultipartFormDataContent files)
        {
            var result = await _httpClient.PutAsync($"https://localhost:7190/api/documents/{documentId}", files);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteDocument(int id)
        {
            var result = await _httpClient.DeleteAsync($"https://localhost:7190/api/documents/{id}");

            return result;
        }

        public async Task<byte[]> DownloadPdf(int id)
        {

            

           var response = await _httpClient.GetAsync($"https://localhost:7190/api/documents/documentPdf/{id}");
           

            return await response.Content.ReadAsByteArrayAsync();
        }


    }
}
