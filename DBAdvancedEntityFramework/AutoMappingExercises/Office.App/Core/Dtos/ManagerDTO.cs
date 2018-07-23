using System;
using System.Collections.Generic;
using System.Text;

namespace Office.App.Core.Dtos
{
    public class ManagerDTO
    {
        public ManagerDTO()
        {
            this.ManagerEmployees = new HashSet<EmployeeDTO>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDTO> ManagerEmployees { get; set; }

        public int EmployeesCount => ManagerEmployees.Count;
    }
}
