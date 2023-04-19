using Microsoft.EntityFrameworkCore;
using RecordsDbLibrary;
using RecordsModels;
using RecordsRepositories.Interfaces;
using RecordsDbLibrary;
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
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentType> GetAllDocumentTypes()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentType> GetAllDocumentTypesById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateDocumentType(DocumentType documentType)
        {
            throw new NotImplementedException();
        }
    }
}
