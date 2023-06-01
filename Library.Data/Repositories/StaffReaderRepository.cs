using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class StaffReaderRepository : BaseRepository<StaffReader>, IStaffReaderRepository
    {
        public StaffReaderRepository(IDbFactory dbFactory) : base(dbFactory) { }

		public async Task<StaffReader> GetLastStaffReader()
		{
            return await dbSet.OrderBy(sr => sr.ID).LastOrDefaultAsync();
		}

		public override Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

    }


    public interface IStaffReaderRepository : IBaseRepository<StaffReader>
    {
		Task<StaffReader> GetLastStaffReader();
	}
}
