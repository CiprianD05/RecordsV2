using RecordsModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace RecordsDTOs.DocumentDTOs
{
    public class DocumentCreateDTO
    {

        public int CitizenId { get; set; }
        public int DocumentTypeId { get; set; }
        public IFormFile Files { get; set; }


    }
}
