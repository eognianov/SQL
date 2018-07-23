using System;
using System.Collections.Generic;
using System.Text;

namespace Office.App.Core.Dtos
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }
    }
}
