using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.DocumentTypeDTOs
{
    public class DocumentTypeCreateDTO
    {
        [Required(ErrorMessage = "Name field is requeired")]
        public string Name { get; set; }
    }
}
