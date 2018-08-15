using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SoftJail.DataProcessor.ExportDto
{
    public class OfficerDto
    {
        public string OfficerName { get; set; }

        public string Department { get; set; }


        [JsonIgnore]

        public decimal Salary { get; set; }
    }
}
