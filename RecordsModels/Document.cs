using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsModels
{
    public class Document
    {
        [Required]
        public int Id{ get; set; }

        [Required]
        public int DocumentTypeId { get; set; }        

        [Required]
        public DateTime DateAdded{ get; set; }

        [Required]
        public string Name { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}
