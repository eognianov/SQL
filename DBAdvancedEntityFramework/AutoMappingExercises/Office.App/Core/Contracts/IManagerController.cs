using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Dtos;

namespace Office.App.Core.Contracts
{
    public interface IManagerController
    {
        void SetManager(int employeeId, int managerId);

        ManagerDTO GetManagerInfo(int employeeId);
    }
}
