using RecordsModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace RecordsDTOs.DocumentDTOs
{
    public class DocumentCreateDTO
    {
        
        [Required]
        public int CitizenId { get; set; }

        [Required]
        public IFormFile File{ get; set; }

        [Required]
        public int DocumentTypeId { get; set; }

               

        public DocumentType DocumentType { get; set; }
        public Citizen Citizen { get; set; }


    }
}
