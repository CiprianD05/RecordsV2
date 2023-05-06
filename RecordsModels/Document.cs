
using System.ComponentModel.DataAnnotations;


namespace RecordsModels
{
    public class Document
    {
        [Required]
        public int Id{ get; set; }

        [Required]
        public int CitizenId { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }        

        [Required]
        public DateTime DateAdded{ get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }


        public DocumentType DocumentType { get; set; }
        public Citizen Citizen { get; set; }
    }
}
