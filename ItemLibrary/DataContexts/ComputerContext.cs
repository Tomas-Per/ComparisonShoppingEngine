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
            => options.UseSqlServer("Data Source=LAPTOP-2JE6GLH9\\SQLEXPRESS;Initial Catalog=LibraDb;Integrated Security=True;");
    }
}
