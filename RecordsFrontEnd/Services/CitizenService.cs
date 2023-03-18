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

        public async Task<HttpResponseMessage> GetCitizen(int id)
        {
            var result = await _http.GetAsync($"https://localhost:7190/api/citizens/{id}");

            return result;
        }

        public async Task<HttpResponseMessage> DeleteCitizen(int id)
        {
            var result = await _http.DeleteAsync($"https://localhost:7190/api/citizens/{id}");

            return result;
        }

        public async Task<HttpResponseMessage> CreateCitizen(CitizenCreateDTO citizenCreateDTO)
        {
            var result = await _http.PostAsJsonAsync($"https://localhost:7190/api/citizens/",citizenCreateDTO);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateCitizen(int id, CitizenUpdateDTO citizenUpdateDTO)
        {
            var result = await _http.PutAsJsonAsync($"https://localhost:7190/api/citizens/{id}", citizenUpdateDTO);

            return result;
        }
    }
}
