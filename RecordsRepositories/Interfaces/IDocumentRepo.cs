using RecordsModels;


namespace RecordsRepositories.Interfaces
{
    public interface IDocumentRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Document>> GetAllDocuments();
        Task<Document> GetAllDocumentById(int Id);
        Task<Document> CreateDocument(Document document);
        void UpdateDocument(Document document);
        void DeleteDocument(Document document);
    }
}
