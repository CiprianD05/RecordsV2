using RecordsDTOs.CitizensDTOs;
using System.Net.Http.Json;
using AutoMapper;

namespace RecordsFrontEnd.Services
{
    public class CitizenService : ICitizenService
    {
        private readonly HttpClient _http;
        private readonly IMapper _mapper;


        public List<CitizenReadDTO> Citizens { get ; set ; }= new List<CitizenReadDTO>();


        public CitizenService(HttpClient http, IMapper mapper)
        {
            _http = http;
            _mapper = mapper;
        }


        public async Task GetCitizens()
        {
            var result = await _http.GetFromJsonAsync<List<CitizenReadDTO>>("https://localhost:7190/api/citizens");

            if (result != null)
                Citizens = result;
        }

        public async Task<CitizenReadDTO> GetCitizen(int id)
        {
            var result = await _http.GetFromJsonAsync<CitizenReadDTO>($"https://localhost:7190/api/citizens/{id}");

            if (result != null)
                return result;

            throw new Exception("Hero not found");
        }

        
    }
}
