using RecordsModels;

namespace RecordsRepositories.Interfaces
{
    interface IDocumentTypeRepo
    {
        Task<bool> SaveChanges();
        IEnumerable<DocumentType> GetAllDocumentTypes();
        Task<DocumentType> GetAllDocumentTypesById(int Id);
        Task<DocumentType> CreateDocumentType(DocumentType documentType);
        void UpdateDocumentType(DocumentType documentType);
        void DeleteDocumentType(DocumentType documentType);
    }
}
