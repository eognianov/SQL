using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using PetClinic.Models;

namespace PetClinic.Dto.ImportDto
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {

        [Required]
        public string Animal { get; set; }


        [Required]
        public string Vet { get; set; }

        [Required]
        public string DateTime { get; set; }

        public AnimalAidProcedureDto[] AnimalAids { get; set; }
    }
}
