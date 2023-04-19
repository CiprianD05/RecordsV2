
using RecordsModels;

namespace RecordsRepositories.Interfaces
{
    public interface ICitizenRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Citizen>> GetAllCitizens();
        Task<Citizen> GetAllCitizenById(int Id);
        Task<Citizen> CreateCitizen(Citizen citizen);
        void UpdateCitizen(Citizen citizen);
        void DeleteCitizen(Citizen citizen);


    }
}
