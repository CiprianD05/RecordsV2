using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordsModels;

namespace RecordsRepositories.Interfaces
{
    public interface ICitizenRepo
    {
        Task<bool> SaveChanges();
        IEnumerable<Citizen> GetAllCitizens();
        Task<Citizen> GetAllCitizenById(int Id);
        Task<Citizen> CreateCitizen(Citizen citizen);
        void UpdateCitizen(Citizen citizen);
        void DeleteCitizen(Citizen citizen);


    }
}
