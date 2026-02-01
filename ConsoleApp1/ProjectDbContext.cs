using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Klasa kontekstu Entity Framework, konfigurująca relacje między tabelami bazy danych.
/// </summary>

namespace ConsoleApp1 {
    public class ProjectDbContext : DbContext {
        public ProjectDbContext() : base("name=ProjectDbContext") {
        }
        public DbSet<Account> Accounts {  get; set; }  
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers {  get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            // Konfiguracja relacji 1:1
            modelBuilder.Entity<Customer>()
                .HasOptional(c => c.PersonalAccount)
                .WithRequired(a => a.Owner);

            modelBuilder.Entity<Transaction>()
                .HasOptional(t => t.Sender)       
                .WithMany(a => a.SentTransactions)
                .HasForeignKey(t => t.SendersPesel)
                .WillCascadeOnDelete(false);   

            modelBuilder.Entity<Transaction>()
                .HasOptional(t => t.Recipient) 
                .WithMany(a => a.ReceivedTransactions)
                .HasForeignKey(t => t.RecipientsPesel)
                .WillCascadeOnDelete(false);
        }
    }
}

