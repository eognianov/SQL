using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;

namespace Office.App.Core.Commands
{
    public class SetAddressCommand:ICommand
    {
        private readonly IEmployeeController controller;

        public SetAddressCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            string address = args[1];

            controller.SetAddress(id, address);

            return $"Command accomplished Successfully!";
        }
    }
}
