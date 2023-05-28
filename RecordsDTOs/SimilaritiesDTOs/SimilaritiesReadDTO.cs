using RecordsDTOs.CitizensDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsDTOs.SimilaritiesDTOs
{
    public class SimilaritiesReadDTO
    {
        public float Score { get; set; }
        public CitizenReadDTO Citizen{ get; set; }
    }
}
