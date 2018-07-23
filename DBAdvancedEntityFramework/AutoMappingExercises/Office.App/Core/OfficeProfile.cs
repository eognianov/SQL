using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Office.App.Core.Dtos;
using Office.Models;

namespace Office.App.Core
{
    public class OfficeProfile:Profile
    {
        public OfficeProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, ManagerDTO>().ReverseMap();
            CreateMap<Employee, EmployeePersenolInfoDTO>().ReverseMap();
        }
    }
}
