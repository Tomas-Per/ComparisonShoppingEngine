using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ModelLibrary.DataContexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Smartphone> Smartphones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Smartphone>()
                .Ignore(x => x.FrontCameras)
                .Ignore(x => x.BackCameras)
                .Ignore(x => x.ScreenDiagonal)
                .Ignore(x => x.Storage)
                .Ignore(x => x.RAM)
                .Ignore(x => x.Processor)
                .Ignore(x => x.Resolution)
                .Ignore(x => x.BatteryStorage)
                .ToTable("Items");
            modelBuilder.Entity<Computer>()
                .Ignore(x => x.Processor)
                .Ignore(x => x.GraphicsCardName)
                .Ignore(x => x.GraphicsCardMemory)
                .Ignore(x => x.RAM)
                .Ignore(x => x.RAM_type)
                .Ignore(x => x.Resolution)
                .Ignore(x => x.StorageCapacity)
                .ToTable("Items");
            modelBuilder.Entity<Item>()
                .HasKey(x => new { x.Id, x.ItemCategory });
            modelBuilder.Entity<Item>()
                .ToTable("Items")
                .HasDiscriminator()
                .IsComplete(false);
        }

    }
}
