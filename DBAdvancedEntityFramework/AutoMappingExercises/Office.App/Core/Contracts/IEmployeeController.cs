using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Dtos;

namespace Office.App.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDTO employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDTO GetEmployeeInfo(int employeeId);

        EmployeePersenolInfoDTO GetEmployeePersenolInfo(int employeeId);
    }
}
