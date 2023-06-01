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

        public override Task UpdateAsync(LogInfo entity)
        {
            var res = dbSet.FirstOrDefault(l => l.UserID == entity.UserID && l.TableName == entity.TableName);
            res.DateUpdated = entity.DateUpdated;
            return Task.CompletedTask;
        }

        public override Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public async Task<LogInfo> GetLastLogID(LogInfo newLog)
        {
            await dbSet.AddAsync(newLog);
            return await dbSet.OrderBy(l=>l.LogID).LastOrDefaultAsync();
        }       
    }

    public interface ILogInfoRepository : IBaseRepository<LogInfo>
    {
        Task<LogInfo> GetLastLogID(LogInfo newLog);
    }
}
