using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.CocumentTypeDTOs
{
    public class DocumentTypeUpdateDTO
    {

        [Required(ErrorMessage = "Name field is requeired")]
        public string Name { get; set; }
    }
}
