using RecordsModels;

namespace RecordsRepositories.Interfaces
{
    public interface IPsychologicalProfileRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<PsychologicalProfile>> GetAllPsychologicalProfiles(int citizenId);
        Task<IEnumerable<PsychologicalProfile>> Get();

        Task<PsychologicalProfile> GetPsychologicalProfileById(int psychologicalProfileId);
        Task<PsychologicalProfile> CreatePsychologicalProfile(PsychologicalProfile psychologicalProfile);
        void UpdatePsychologicalProfile(PsychologicalProfile psychologicalProfile);
        void DeletePsychologicalProfile(PsychologicalProfile psychologicalProfile);


    }
}
