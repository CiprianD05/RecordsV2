﻿
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
            context.Citizens.Remove(citizen);
        }

        public async Task<Citizen> GetAllCitizenById(int Id)
        {
            return await context.Citizens.SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<IEnumerable<Citizen>> GetAllCitizens()
        {
            return await context.Citizens.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateCitizen(Citizen citizen)
        {

           
        }
    }
}
