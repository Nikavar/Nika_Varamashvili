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

        //public async Task<StaffReader> GetByIdAsync(int id)
        //{
        //    return await _dbContext.FindAsync(typeof(StaffReader), id);
        //}


    }


    public interface IStaffReaderRepository : IBaseRepository<StaffReader>
    {
        
    }
}
