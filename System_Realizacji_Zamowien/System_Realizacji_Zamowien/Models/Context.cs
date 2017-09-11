using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("Connection")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Access> Accesses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ItemCard> ItemCards { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Firm> Firms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UnitOfMeasure> Units { get; set; }

        public DbSet<State> States{ get; set; }
    }
}