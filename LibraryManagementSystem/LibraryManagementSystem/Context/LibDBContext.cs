using Library.Model.Models;
using LibraryManagementSystem.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Context
{
    public class LibDBContext : DbContext
    {
        //public LibDBContext(DbContextOptions<LibDBContext> options) : base(options)
        //{            
        //}

        public DbSet<User> UserLogin { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>(entity => { entity.HasKey(k => k.UserID); });
        //}

        public LibDBContext(DbContextOptions<LibDBContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Database.EnsureCreated();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("LibraryDbConnection");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }



    //public class LibDBContext : IdentityDbContext
    //{
    //    public LibDBContext(DbContextOptions<LibDBContext> options) : base(options)
    //    {
    //        if (options is null)
    //        {
    //            throw new ArgumentNullException(nameof(options));
    //        }

    //        Database.EnsureCreated();
    //    }
    //    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //{
    //    //    optionsBuilder.UseSqlServer("LibraryDbConnection");
    //    //}

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibDBContext).Assembly);
    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
}
