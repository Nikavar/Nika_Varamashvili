using Library.Data.Configuration;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base()
        {

        }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<RoleUser>? UserRoles { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<StaffReader>? StaffReaders { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<ReaderStatus>? ReaderStatuses { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Tab>? Tabs { get; set; }
        public DbSet<BookStorage>? BookStorages { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Email>? EmailTemplates { get; set; }
        public DbSet<Storage>? Storages { get; set; }
        public DbSet<LogInfo>? LogInfo { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new ReaderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new StaffReaderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleUserConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new LogInfoConfiguration());
            modelBuilder.ApplyConfiguration(new TabConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            //modelBuilder.ApplyConfiguration(new BookPublisherConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmailConfiguration());



            modelBuilder.Entity<User>().HasData(
                 new List<User>
                {
                    new User{ id = 1, Email = "user1", Password = "123",StaffReaderID = 0 },
                    new User{ id = 2, Email = "user2", Password= "345",StaffReaderID = 0 }
                }
            );
        }
    }
}
