using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;

namespace Office.App.Core.Commands
{
    public class ManagerInfoCommand:ICommand
    {
        private readonly IManagerController controller;

        public ManagerInfoCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            int employeeId = int.Parse(args[0]);

            var managerDto = this.controller.GetManagerInfo(employeeId);

            StringBuilder result = new StringBuilder();

            result.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesCount}");

            foreach (var employeeDto in managerDto.ManagerEmployees)
            {
                result.AppendLine($"\t - {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
