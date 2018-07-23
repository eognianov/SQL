using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Office.App.Core;
using Office.App.Core.Contracts;
using Office.App.Core.Controllers;
using Office.Data;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var service = ConfigureService();
            IEngine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<OfficeContext>(opts => opts.UseSqlServer(Configuration.ConnectionString));
            serviceCollection.AddTransient<IDbInitiliazerService, DbInitiliazerService>();
            serviceCollection.AddTransient<ICommandIntrepreter, CommandIntrepreter>();
            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();
            serviceCollection.AddAutoMapper(conf => conf.AddProfile<OfficeProfile>());
            serviceCollection.AddTransient<IManagerController, ManagerController>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;

        }
    }
}
