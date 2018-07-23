using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Office.App.Core.Contracts;

namespace Office.App.Core.Commands
{
    public class ExitCommand:ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i >= 0; i--)
            {
                Console.WriteLine($"Program will close after {i} seconds!");
                Thread.Sleep(1000);
            }

            Environment.Exit(0);

            return string.Empty;
        }
    }
}
