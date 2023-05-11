using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class PositionRepository : RepositoryBase<Position>, IPositionInterface
    {
        public PositionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Position GetById(int id)
        {
            var position = this.DbContext.Positions.Where(p => p.PositionID == id).FirstOrDefault();
            return position ?? throw new NotImplementedException();
        }
    }


    public interface IPositionInterface : IRepository<Position>
    {

    }
}
