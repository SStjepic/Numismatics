using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.INFRASTRUCTURE.Repositories.SQLite
{
    public class SQLRepositoryContext: DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Banknote> Banknotes { get; set; }
        public DbSet<NationalCurrency> Nationalcurrencies { get; set; }
        public DbSet<OwnedBanknote> OwnedBanknotes { get; set; }
        public DbSet<OwnedCoin> OwnedCoins { get; set; }

        public SQLRepositoryContext(DbContextOptions<SQLRepositoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dateConverter = new ValueConverter<Date, string>(
                d => d.ToString(),         
                s => Date.Parse(s)         
            );

            modelBuilder.Entity<Country>().HasKey(c => c.Id);
            modelBuilder.Entity<Country>()
                .Property(c => c.StartYear)
                .HasConversion(dateConverter);

            modelBuilder.Entity<Country>()
                .Property(c => c.EndYear)
                .HasConversion(dateConverter);

            modelBuilder.Entity<Currency>().HasKey(c => c.Id);

            modelBuilder.Entity<Coin>().HasKey(c => c.Id);
            modelBuilder.Entity<Coin>()
                .Property(c => c.IssueDate)
                .HasConversion(dateConverter);

            modelBuilder.Entity<Banknote>().HasKey(b => b.Id);
            modelBuilder.Entity<Banknote>()
                .Property(b => b.IssueDate)
                .HasConversion(dateConverter);

            modelBuilder.Entity<NationalCurrency>().HasKey(b => b.Id);

            modelBuilder.Entity<OwnedBanknote>().HasKey(c => c.Id);
            modelBuilder.Entity<OwnedBanknote>()
                .Property(b => b.Quality)
                .HasConversion<string>();


            modelBuilder.Entity<OwnedCoin>().HasKey(b => b.Id);
            modelBuilder.Entity<OwnedCoin>()
                .Property(c => c.Quality)
                .HasConversion<string>();


        }
    }
}
