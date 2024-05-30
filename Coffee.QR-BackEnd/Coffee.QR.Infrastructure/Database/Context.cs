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
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Local> Locals { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }  
        public DbSet<Card> Cards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CardUser> CardUsers { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<CardSaleReport> CardSaleReports { get; set; }

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

            modelBuilder.Entity<Event>()
            .HasOne(l => l.Local)
            .WithMany()
            .HasForeignKey(l => l.LocalId)
            .IsRequired();

            modelBuilder.Entity<Table>()
            .HasOne(t => t.Place)
            .WithMany()
            .HasForeignKey(t => t.LocalId)
            .IsRequired();

            modelBuilder.Entity<Notification>()
            .HasOne(n => n.TableOrigin)
            .WithMany()
            .HasForeignKey(n => n.TableId)
            .IsRequired();

            modelBuilder.Entity<Notification>()
            .HasOne(n => n.Place)
            .WithMany()
            .HasForeignKey(n => n.LocalId)
            .IsRequired();

            modelBuilder.Entity<Card>()
            .HasOne(c => c.@event)
            .WithMany()
            .HasForeignKey(c => c.EventId)
            .IsRequired();
            
            modelBuilder.Entity<Order>()
            .HasOne(t => t.TableOrigin)
            .WithMany()
            .HasForeignKey(t => t.TableId)
            .IsRequired();

            modelBuilder.Entity<Order>()
            .HasOne(l => l.Local)
            .WithMany()
            .HasForeignKey(l => l.LocalId)
            .IsRequired();

            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.OrderOrigin)
            .WithMany()
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired();

            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.ItemPicked)
            .WithMany()
            .HasForeignKey(oi => oi.ItemId)
            .IsRequired();

            modelBuilder.Entity<CardUser>()
            .HasOne(cu => cu.Card)
            .WithMany()
            .HasForeignKey(cu => cu.CardId)
            .IsRequired();

            modelBuilder.Entity<CardUser>()
            .HasOne(cu => cu.User)
            .WithMany()
            .HasForeignKey(cu => cu.UserId)
            .IsRequired();

            modelBuilder.Entity<Receipt>()
            .HasOne(r => r.Order)
            .WithMany()
            .HasForeignKey(r => r.OrderId)
            .IsRequired();

            modelBuilder.Entity<Receipt>()
            .HasOne(r => r.Waiter)
            .WithMany()
            .HasForeignKey(r => r.WaiterId)
            .IsRequired();

            modelBuilder.Entity<CardSaleReport>()
            .HasOne(cu => cu.User)
            .WithMany()
            .HasForeignKey(cu => cu.UserId)
            .IsRequired();

            Configure(modelBuilder);
        }

        private static void Configure(ModelBuilder modelBuilder)
        {

        }
    }
}
