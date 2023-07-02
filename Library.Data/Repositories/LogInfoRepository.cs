using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class LogInfoRepository : BaseRepository<LogInfo>, ILogInfoRepository
    {
        public LogInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }
        public override Task<LogInfo> AddAsync(LogInfo entity)
        {
            return base.AddAsync(entity);
        }

        public override async Task UpdateAsync(LogInfo entity)
        {
            if(entity != null)
            {
                entity.DateUpdated = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                await base.UpdateAsync(entity);
            }           
        }

        public override Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public override async Task DeleteAsync(LogInfo entity)
        {
            entity.DateDeleted = DateTime.Now;
            await base.UpdateAsync(entity);
        }

        public override async Task DeleteManyAsync(Expression<Func<LogInfo, bool>> filter)
        {
            var list = await GetManyAsync(filter);
            foreach (var item in list)
            {
                item.DateDeleted = DateTime.Now;
                await base.UpdateAsync(item);
            }
        }
        public async Task<LogInfo> GetLogByEntityId(int id)
        {
            var logEntity = await dbSet.Where(x=>x.EntityID == id).FirstOrDefaultAsync();
            return logEntity != null ? logEntity : new LogInfo();
        }
    }

    public interface ILogInfoRepository : IBaseRepository<LogInfo>
    {
        Task<LogInfo> GetLogByEntityId(int id);
    }
}
