using RecordsModels;
using RecordsRepositories.Interfaces;
using RecordsDbLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecordsRepositories.ConcretRepos
{
    public class SqlDocumentRepo:IDocumentRepo
    {
        private readonly RecordsDbContext _context;

        public SqlDocumentRepo(RecordsDbContext context)
        {
            _context = context;    
        }

        public async Task<Document> CreateDocument(Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            EntityEntry<Document> entityEntry = await _context.AddAsync(document);

            return entityEntry.Entity;
        }

        public void DeleteDocument(Document document)
        {
            _context.Documents.Remove(document);
        }

        public async Task<IEnumerable<Document>> GetAllDocuments(int id)
        {
            return await _context.Documents.Where(d=>d.Id==id).ToListAsync();
        }

        public async Task<Document> GetAllDocumentById(int Id)
        {
            return await _context.Documents.SingleOrDefaultAsync(d => d.Id == Id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void UpdateDocument(Document document)
        {
            
        }
    }
}
