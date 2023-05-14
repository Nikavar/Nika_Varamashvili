using Library.Data.Configuration;
using Library.Model.Models;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Library.Data
{
    public class LibraryEntities : DbContext
    {
        public LibraryEntities() : base("LibraryManagementDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StaffReader> StaffReaders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ReaderStatus> ReaderStatuses { get; set; }
        public DbSet<LogInfo> Logs { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new StaffReaderConfiguration());
        }
    }
}
