using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;
using Office.App.Core.Dtos;

namespace Office.App.Core.Commands
{
    public class AddEmployeeCommand:ICommand
    {
        private readonly IEmployeeController controller;

        public AddEmployeeCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDTO employeeDto = new EmployeeDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.controller.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} was added successfully!";
        }
    }
}
