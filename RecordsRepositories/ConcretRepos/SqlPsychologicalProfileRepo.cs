using RecordsModels;
using RecordsRepositories.Interfaces;
using RecordsDbLibrary;
using Microsoft.EntityFrameworkCore;

namespace RecordsRepositories.ConcretRepos
{
    public class SqlPsychologicalProfileRepo : IPsychologicalProfileRepo
    {
        private readonly RecordsDbContext _context;

        public SqlPsychologicalProfileRepo(RecordsDbContext context)
        {
            _context = context;
        }


        public async Task<PsychologicalProfile> CreatePsychologicalProfile(PsychologicalProfile psychologicalProfile)
        {
            if (psychologicalProfile==null)
                throw new ArgumentNullException(nameof(psychologicalProfile));

            var entityEntry = await _context.AddAsync(psychologicalProfile);

            return entityEntry.Entity;
        }

        public void DeletePsychologicalProfile(PsychologicalProfile psychologicalProfile)
        {
            _context.PsychologicalProfiles.Remove(psychologicalProfile);
        }

        public async Task<IEnumerable<PsychologicalProfile>> GetAllPsychologicalProfiles(int citizenId)
        {
            return await _context.PsychologicalProfiles.Include(pp=>pp.Citizen).Where(pp=>pp.CitizenId==citizenId).ToListAsync(); 

        }

        public async Task<PsychologicalProfile> GetPsychologicalProfileById(int psychologicalProfileId)
        {
            return await _context.PsychologicalProfiles.Include(pp => pp.Citizen).Where(pp => pp.Id == psychologicalProfileId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<PsychologicalProfile>> Get()
        {
            return await _context.PsychologicalProfiles.Include(pp => pp.Citizen).ToListAsync();

        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void UpdatePsychologicalProfile(PsychologicalProfile psychologicalProfile)
        {
            
        }
    }
}
