using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public class ProjectDbContext : DbContext {
        public DbSet<Account> Accounts {  get; set; }  
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Employee> Employeees { get; set; }
        public DbSet<Customer> Customers {  get; set; }
    }
}

