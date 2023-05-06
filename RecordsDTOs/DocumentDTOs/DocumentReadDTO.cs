using RecordsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.DocumentDTOs
{
    public class DocumentReadDTO
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }

        

        [Required]
        public string Name { get; set; }


        public DocumentType DocumentType { get; set; }
        public Citizen Citizen { get; set; }

    }
}
