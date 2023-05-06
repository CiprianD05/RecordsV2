using Microsoft.AspNetCore.Http;
using RecordsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.DocumentDTOs
{
    public class DocumentUpdateDTO
    {
        
        [Required]
        public int CitizenId { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }


        public IFormFile File { get; set; }

        


        public DocumentType DocumentType { get; set; }
        public Citizen Citizen { get; set; }

    }
}
