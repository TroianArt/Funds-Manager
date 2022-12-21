using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using DAL.Domain;

namespace DAL.Context
{
    /// <summary>
    /// Fund Context class
    /// Contains fields Users, BankAccounts, Currency, Transaction and
    /// </summary>
    public sealed class FundsContext : DbContext
    {
        /// <summary>
        /// Gets or sets funds Context set of Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets funds Context set of Bank accounts
        /// </summary>
        public DbSet<BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// Gets or sets funds Context set of Currency
        /// </summary>
        public DbSet<Currency> Currency { get; set; }

        /// <summary>
        /// Gets or sets funds Context set of transactions
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FundsContext"/> class.
        /// </summary>
        public FundsContext()

            : base(new DbContextOptionsBuilder<FundsContext>().UseSqlServer(@Environment.GetEnvironmentVariable("FundsManagerDB")).UseLazyLoadingProxies().Options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FundsContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options</param>
        public FundsContext(DbContextOptions<FundsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Set table relationships
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBankAccount>().HasKey(t => new {t.UserId, t.BankAccountId});

            modelBuilder.Entity<UserBankAccount>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.BankAccounts)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserBankAccount>()
                .HasOne(pt => pt.BankAccount)
                .WithMany(p => p.Users)
                .HasForeignKey(pt => pt.BankAccountId);

            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.Mail).IsUnique();
            });
        }
    }
}