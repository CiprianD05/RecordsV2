using RecordsDTOs.PsychologicalProfileDTOs;

namespace RecordsFrontEnd.Services
{
    public interface IPsychologicalProfileService
    {

        List<PsychologicalProfileReadDTO> PsychologicalProfiles { get; set; }

        Task GetIPsychologicalProfiles(int citizenId);

        Task<HttpResponseMessage> GetIPsychologicalProfile(int id);
        Task<HttpResponseMessage> DeleteIPsychologicalProfile(int id);

        Task<HttpResponseMessage> CreateIPsychologicalProfile(PsychologicalProfileCreateDTO psychologicalProfileCreateDTO);

        Task<HttpResponseMessage> UpdateIPsychologicalProfile(int id, PsychologicalProfileUpdateDTO psychologicalProfileUpdateDTO);
    }
}
