using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        LibraryContext? dbContext;

        private readonly DbContextOptions<LibraryContext> options;

        public DbFactory(DbContextOptions<LibraryContext> options)
        {
            this.options = options;
        }
        public LibraryContext Init()
        {
            return dbContext ?? (dbContext = new LibraryContext(options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
