using P01_StudentSystem.Data;
using System;

namespace P01_StudentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (StudentSystemContext contex = new StudentSystemContext())
            {
                contex.Database.EnsureDeleted();
                contex.Database.EnsureCreated();
            }
        }
    }
}
