using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlightManager.Data
{
    public class FlightManagerDbContext : IdentityDbContext<AppUser>
    {
        public FlightManagerDbContext() { }

        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options) { }

        //Adding dbsets
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        /// <summary>
        /// This method checks if the optionsBuilder is not already configured and 
        /// if it is not, configures it by connencting to the database via connection string.
        /// </summary>
        /// <param name="optionsBuilder">optionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationData.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// This method calls the OnModelCreating method of the DbContext class that is inherited.
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
