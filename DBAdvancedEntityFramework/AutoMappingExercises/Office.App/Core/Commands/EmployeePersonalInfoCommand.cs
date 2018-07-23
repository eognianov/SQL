using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;
using Office.App.Core.Dtos;

namespace Office.App.Core.Commands
{
    public class EmployeePersonalInfoCommand:ICommand
    {
        private readonly IEmployeeController controller;

        public EmployeePersonalInfoCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            StringBuilder result = new StringBuilder();
            int id = int.Parse(args[0]);
            EmployeePersenolInfoDTO employeeDto = controller.GetEmployeePersenolInfo(id);

            result.AppendLine(
                $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
            result.AppendLine($"Birthday: {employeeDto.Birthday.Value.ToString("dd-MM-yyyy")}");
            result.AppendLine($"Address: {employeeDto.Address}");

            return result.ToString().TrimEnd();
        }
    }
}
