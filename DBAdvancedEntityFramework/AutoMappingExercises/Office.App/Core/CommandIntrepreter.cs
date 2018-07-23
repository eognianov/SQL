using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Office.App.Core.Contracts;

namespace Office.App.Core
{
    public class CommandIntrepreter:ICommandIntrepreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandIntrepreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            string commandName = input[0] + "Command";
            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x=>x.Name==commandName);

            if (type==null)
            {
                throw new ArgumentException("Invalid command!");
            }

            var constructor = type.GetConstructors().First();

            var constructorParams = constructor.GetParameters().Select(x => x.ParameterType).ToArray();

            var service = constructorParams.Select(serviceProvider.GetService).ToArray();

            var command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);

            return result;
        }
    }
}
