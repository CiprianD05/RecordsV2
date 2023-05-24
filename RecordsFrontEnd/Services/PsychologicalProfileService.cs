
using RecordsDTOs.PsychologicalProfileDTOs;
using System.Net.Http.Json;


namespace RecordsFrontEnd.Services
{
    public class PsychologicalProfileService : IPsychologicalProfileService
    {
        private readonly HttpClient _httpClient;

        public List<PsychologicalProfileReadDTO> PsychologicalProfiles { get; set; } = new List<PsychologicalProfileReadDTO>();



        public PsychologicalProfileService(HttpClient  http)
        {
            _httpClient = http;
        }


        public async Task GetIPsychologicalProfiles(int citizenId)
        {
            

            var result = await _httpClient.GetFromJsonAsync<List<PsychologicalProfileReadDTO>>($"https://localhost:7190/api/PsychologicalProfiles/citizenId/{citizenId}");

            if (result != null)
                PsychologicalProfiles = result;
        }


        public async Task<HttpResponseMessage> GetIPsychologicalProfile(int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7190/api/PsychologicalProfiles/{id}");

            return result;
        }


        public async Task<HttpResponseMessage> CreateIPsychologicalProfile(PsychologicalProfileCreateDTO psychologicalProfileCreateDTO)
        {
            var result = await _httpClient.PostAsJsonAsync($"https://localhost:7190/api/PsychologicalProfiles/", psychologicalProfileCreateDTO);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteIPsychologicalProfile(int id)
        {
            var result = await _httpClient.DeleteAsync($"https://localhost:7190/api/PsychologicalProfiles/{id}");

            return result;
        }

        

        

        public async Task<HttpResponseMessage> UpdateIPsychologicalProfile(int id, PsychologicalProfileUpdateDTO psychologicalProfileUpdateDTO)
        {
            var result = await _httpClient.PutAsJsonAsync($"https://localhost:7190/api/PsychologicalProfiles/{id}", psychologicalProfileUpdateDTO);

            return result;
        }
    }
}
