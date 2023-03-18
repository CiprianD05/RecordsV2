using RecordsDTOs.CitizensDTOs;

namespace RecordsFrontEnd.Services
{
    public interface ICitizenService
    {

        List<CitizenReadDTO> Citizens { get; set; }

        Task GetCitizens();

        Task<HttpResponseMessage> GetCitizen(int id);
        Task<HttpResponseMessage> DeleteCitizen(int id);

        Task<HttpResponseMessage> CreateCitizen(CitizenCreateDTO citizenCreateDTO);

        Task<HttpResponseMessage> UpdateCitizen(int id,CitizenUpdateDTO citizenCreateDTO);



    }
}
