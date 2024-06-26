﻿using RecordsModels;
using RecordsRepositories.Interfaces;
using RecordsDbLibrary;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            return await _context.Documents.Where(d=>d.CitizenId==id).Include(d=>d.Citizen).ToListAsync();
        }

        public async Task<Document> GetAllDocumentById(int Id)
        {
            return await _context.Documents.Include(d=>d.Citizen).Include(d=>d.DocumentType).SingleOrDefaultAsync(d => d.Id == Id);
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
