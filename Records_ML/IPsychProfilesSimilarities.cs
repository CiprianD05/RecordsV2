using RecordsDTOs.SimilaritiesDTOs;
using RecordsModels;

namespace Records_ML
{
    public interface IPsychProfilesSimilarities
    {
          Task<List<SimilaritiesReadDTO>> CompareProfiles(Citizen targetCitizen);
    }
}
