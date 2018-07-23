using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;

namespace Office.App.Core.Commands
{
    public class SetBirthdayCommand:ICommand
    {
        private readonly IEmployeeController controller;

        public SetBirthdayCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);
            this.controller.SetBirthday(id, date);

            return $"Command accomplished successfully!";
        }
    }
}
