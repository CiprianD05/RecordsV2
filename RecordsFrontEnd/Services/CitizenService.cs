using RecordsDTOs.CitizensDTOs;
using System.Net.Http.Json;

namespace RecordsFrontEnd.Services
{
    public class CitizenService : ICitizenService
    {
        private readonly HttpClient _http;     


        public List<CitizenReadDTO> Citizens { get ; set ; }= new List<CitizenReadDTO>();


        public CitizenService(HttpClient http)
        {
            _http = http;
        }


        public async Task GetCitizens()
        {
            var result = await _http.GetFromJsonAsync<List<CitizenReadDTO>>("https://localhost:7190/api/citizens");

            if (result != null)
                Citizens = result;
        }

        public Task<CitizenReadDTO> GetCitizen(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
