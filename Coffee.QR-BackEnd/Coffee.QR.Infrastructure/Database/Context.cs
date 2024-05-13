using Coffee.QR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Item> Items { get; set; }  
        public DbSet<Company> Companies { get;set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }    

        public Context(DbContextOptions<Context> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CoffeeQRSchema");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Event>()
            .HasOne(e => e.Creator)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

            Configure(modelBuilder);
        }

        private static void Configure(ModelBuilder modelBuilder)
        {

        }
    }
}
