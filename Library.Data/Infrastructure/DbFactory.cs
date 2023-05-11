using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        LibraryEntities dbContext;
        public LibraryEntities Init()
        {
            return dbContext ?? (dbContext = new LibraryEntities());
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
