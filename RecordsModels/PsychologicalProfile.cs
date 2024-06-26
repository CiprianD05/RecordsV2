﻿using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsModels
{
    public class PsychologicalProfile
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CitizenId { get; set; }

        [Required] 
        public string Psychologist { get; set; }

        [Required]
        public DateTime DateAdded{ get; set; }

        [Required]
        public string Summary { get; set; }

        [NoColumn]
        public Citizen Citizen { get; set; }
    }
}
