using System;
using Microsoft.EntityFrameworkCore;
using Office.Data;
using Office.Services.Contracts;

namespace Office.Services
{
    public class DbInitiliazerService:IDbInitiliazerService
    {
        private readonly OfficeContext context;
        public DbInitiliazerService(OfficeContext context)
        {
            this.context = context;
        }
        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
