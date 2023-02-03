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
        bool SaveChanges();
        IEnumerable<Citizen> GetAllCitizens();
        Citizen GetAllCitizenById(int Id);
        void CreateCitizen(Citizen citizen);
        void UpdateCitizen(Citizen citizen);
        void DeleteCitizen(Citizen citizen);


    }
}
