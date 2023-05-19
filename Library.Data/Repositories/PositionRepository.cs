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
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        //public Position GetById(int id)
        //{
        //    var position = this.DbContext.Positions.Where(p => p.PositionID == id).FirstOrDefault();
        //    return position ?? throw new NotImplementedException();
        //}
    }


    public interface IPositionRepository : IBaseRepository<Position>
    {

    }
}
