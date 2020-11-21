using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary.DataContexts
{
    public class ComputerContext :DbContext
    {
        public ComputerContext() : base() { }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Processor> Processors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:libradb.database.windows.net,1433;Initial Catalog=LibraDb;Persist Security Info=False;User ID=adminlibra;Password={Libra123!};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
