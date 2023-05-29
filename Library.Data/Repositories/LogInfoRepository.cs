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
    public class LogInfoRepository : BaseRepository<LogInfo>, ILogInfoRepository
    {
        public LogInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        //public LogInfo GetById(int id)
        //{
        //    //var log = this.DbContext.Logs.Where(h => h.LogID == id).FirstOrDefault();
        //    //return log ?? throw new NotImplementedException();
        //}
    }

    public interface ILogInfoRepository : IBaseRepository<LogInfo>
    {

    }
}
