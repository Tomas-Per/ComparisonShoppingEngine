using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary.DataContexts
{
    public class SmartphoneContext :DbContext
    {
        public SmartphoneContext(DbContextOptions<SmartphoneContext> options) : base(options) { }
        public DbSet<Smartphone> Smartphones { get; set; }
    }
}
