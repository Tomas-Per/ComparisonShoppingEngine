using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.DataContexts
{
    public class ComputerContext :DbContext
    {
        public ComputerContext(DbContextOptions<ComputerContext> options) : base(options) { }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Processor> Processors { get; set; }
        
    }
}
