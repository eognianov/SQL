using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Office.App.Core.Contracts;
using Office.App.Core.Dtos;
using Office.Data;
using Office.Models;

namespace Office.App.Core.Controllers
{
    public class EmployeeController:IEmployeeController
    {
        private readonly OfficeContext context;
        private readonly IMapper mapper;

        public EmployeeController(OfficeContext context, IMapper mapper)
        { 
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDTO employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Birthday = date;
            context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee==null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Address = address;
            context.SaveChanges();
        }

        public EmployeeDTO GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeeDTO>(employee);

            if (employee==null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }

        public EmployeePersenolInfoDTO GetEmployeePersenolInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeePersenolInfoDTO>(employee);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }
    }
}
