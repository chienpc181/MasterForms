using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using MasterForms.Entities;

namespace MasterForms
{
    public class MasterFormsDBContext : DbContext
    {
        static private string s_migrationSqlitePath;
        public static DbConnection dbConnection;

        static MasterFormsDBContext()
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            s_migrationSqlitePath = $"{exeDir}MasterFormsDB.sqlite";
            dbConnection = new SQLiteConnection($"DATA Source={s_migrationSqlitePath}");
        }

        public MasterFormsDBContext() : base(dbConnection, false)
        {
        }

        public MasterFormsDBContext(DbConnection connection) : base(connection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Customer>();
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankUser> BankUsers { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }

}
