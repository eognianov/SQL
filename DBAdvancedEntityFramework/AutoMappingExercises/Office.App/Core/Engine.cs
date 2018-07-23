using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Core.Contracts;
using Office.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Office.App.Core
{
    public class Engine:IEngine
    {
        private readonly IServiceProvider serviceProvider;
        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            var initilizeDb = this.serviceProvider.GetService<IDbInitiliazerService>();
            initilizeDb.InitializeDatabase();
            var commandInrepreter = this.serviceProvider.GetService<ICommandIntrepreter>();

            while (true)
            {
                string[] input = Console.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                string result = commandInrepreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}
