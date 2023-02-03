using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordsDbLibrary;
using RecordsModels;
using RecordsRepositories.Interfaces;

namespace RecordsRepositories.ConcretRepos
{
    public class SqlCitizensRepo : ICitizenRepo
    {
        private RecordsDbLibrary.RecordsDbContext context;

        public SqlCitizensRepo(RecordsDbContext context)
        {
            this.context = context;
        }

        public void CreateCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public void DeleteCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Citizen GetAllCitizenById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Citizen> GetAllCitizens()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }
    }
}
