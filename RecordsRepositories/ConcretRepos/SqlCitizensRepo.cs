using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Citizen> CreateCitizen(Citizen citizen)
        {
            if (citizen == null)
            {
                throw new ArgumentNullException(nameof(citizen));
            }

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Citizen> entityEntry = await context.AddAsync(citizen);
            return entityEntry.Entity;
        }

        public void DeleteCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public async Task<Citizen> GetAllCitizenById(int Id)
        {
            return await context.Citizens.SingleOrDefaultAsync(c => c.Id == Id);
        }

        public IEnumerable<Citizen> GetAllCitizens()
        {
            return context.Citizens;
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }
    }
}
