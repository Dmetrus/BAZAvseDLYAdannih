using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US_5A_Net.models;

namespace US_5A_Net
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Flights> FlightersBD { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        {
           
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flights>().HasKey(u => u.id);
        }
    }
}
