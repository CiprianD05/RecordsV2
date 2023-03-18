using RecordsDTOs.CitizensDTOs;

namespace RecordsFrontEnd.Services
{
    public interface ICitizenService
    {

        List<CitizenReadDTO> Citizens { get; set; }

        Task GetCitizens();

        Task<HttpResponseMessage> GetCitizen(int id);
    }
}
