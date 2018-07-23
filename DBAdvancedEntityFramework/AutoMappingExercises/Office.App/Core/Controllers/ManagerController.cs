using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Office.App.Core.Contracts;
using Office.App.Core.Dtos;
using Office.Data;
using Office.Models;

namespace Office.App.Core.Controllers
{
    public class ManagerController:IManagerController
    {
        private readonly OfficeContext context;
        private readonly IMapper mapper;

        public ManagerController(OfficeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var manager = this.context.Employees.Find(managerId);

            if (employee==null || manager == null)
            {
                throw new  ArgumentException("Invalid Id!");
            }

            employee.Manager = manager;
            context.SaveChanges();
        }

        public ManagerDTO GetManagerInfo(int employeeId)
        {
            Employee employee = context.Employees.Include(x=>x.ManagerEmployees).Where(x=>x.Id==employeeId).FirstOrDefault();

            if (employee==null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var managerDto = mapper.Map<ManagerDTO>(employee);

            return managerDto;
        }
    }
}
