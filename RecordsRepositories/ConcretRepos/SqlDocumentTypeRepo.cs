using Microsoft.EntityFrameworkCore;
using RecordsDbLibrary;
using RecordsModels;
using RecordsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RecordsRepositories.ConcretRepos
{
    public class SqlDocumentTypeRepo : IDocumentTypeRepo
    {
        private RecordsDbContext _context;

        public SqlDocumentTypeRepo(RecordsDbContext context)
        {
            this._context = context;
        }

        public async Task<DocumentType> CreateDocumentType(DocumentType documentType)
        {
            if(documentType==null)
                throw new ArgumentNullException(nameof(documentType));

            EntityEntry<DocumentType> entityEntry = await _context.AddAsync(documentType);

            return entityEntry.Entity;
        }

        public void DeleteDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Remove(documentType);
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypes()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<DocumentType> GetAllDocumentTypesById(int Id)
        {
            return await _context.DocumentTypes.SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateDocumentType(DocumentType documentType)
        {
            
        }
    }
}
