using RecordsDTOs.PsychologicalProfileDTOs;
using RecordsDTOs.SimilaritiesDTOs;
using RecordsFrontEnd.Pages;
using System.Net.Http.Json;

namespace RecordsFrontEnd.Services
{
    public class PsychologicalProfileSimilaritiesService : IPsychologicalProfileSimilaritiesService
    {
        private readonly HttpClient _httpClient;
        public List<SimilaritiesReadDTO> PsychologicalProfileSimilarities { get ; set ; }= new List<SimilaritiesReadDTO>();


        public PsychologicalProfileSimilaritiesService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task GetPsychologicalProfilesSimilarities(int citizenId)
        {


            var result = await _httpClient.GetFromJsonAsync<List<SimilaritiesReadDTO>>($"https://localhost:7190/api/Citizens/citizenId/{citizenId}");

            if (result != null)
                PsychologicalProfileSimilarities = result;
        }

       
    }
}
