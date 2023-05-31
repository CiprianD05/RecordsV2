using RecordsDTOs.PsychologicalProfileDTOs;
using RecordsDTOs.SimilaritiesDTOs;

namespace RecordsFrontEnd.Services
{
    public interface IPsychologicalProfileSimilaritiesService
    {
        List<SimilaritiesReadDTO> PsychologicalProfileSimilarities { get; set; }
        Task GetPsychologicalProfilesSimilarities(int citizenId);
    }
}
