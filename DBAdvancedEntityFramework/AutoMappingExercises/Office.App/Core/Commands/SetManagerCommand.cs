using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;

namespace Office.App.Core.Commands
{
    public class SetManagerCommand:ICommand
    {
        private readonly IManagerController controller;

        public SetManagerCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            this.controller.SetManager(employeeId, managerId);

            return "Command accomplished succssefully!";
        }
    }
}
