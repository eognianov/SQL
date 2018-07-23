using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;
using Office.App.Core.Dtos;

namespace Office.App.Core.Commands
{
    public class EmployeeInfoCommand:ICommand
    {
        private readonly IEmployeeController controller;

        public EmployeeInfoCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            EmployeeDTO employeeDto = controller.GetEmployeeInfo(id);

            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}";
        }
    }
}
