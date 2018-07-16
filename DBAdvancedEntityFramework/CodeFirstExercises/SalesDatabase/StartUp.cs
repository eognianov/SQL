using System;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (DbContext context = new SalesContext())
            {
                
            }
        }
    }
}
