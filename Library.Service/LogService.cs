using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface ILogService
    {
        Task<IEnumerable<LogInfo>> GetAllLogsAsync();
        Task<IEnumerable<LogInfo>> GetManyLogsAsync(Expression<Func<LogInfo, bool>> filter);
        Task<LogInfo> GetLogByIdAsync(int id);
        Task<LogInfo> AddLogAsync(LogInfo entity);
        Task UpdateLogAsync(LogInfo entity);
        Task DeleteLogAsync(LogInfo entity);
        Task DeleteManyLogsAsync(Expression<Func<LogInfo, bool>> filter);
        //Task<LogInfo> GetLastLogID(LogInfo entity);
        Task SaveLogsAsync();
    }


    public class LogService : ILogService
    {
        private readonly ILogInfoRepository _logInfoRepository;
        private readonly IUnitOfWork unitOfWork;

        public LogService(ILogInfoRepository logRepo)
        {
            _logInfoRepository = logRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<LogInfo> AddLogAsync(LogInfo entity)
        {
            return await _logInfoRepository.AddAsync(entity);
        }

        public async Task DeleteLogAsync(LogInfo entity)
        {
            await _logInfoRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyLogsAsync(Expression<Func<LogInfo, bool>> filter)
        {
            await _logInfoRepository.DeleteManyAsync(filter);
        }

        public async Task<IEnumerable<LogInfo>> GetAllLogsAsync()
        {
            return await _logInfoRepository.GetAllAsync();
        }

        public async Task<LogInfo> GetLogByIdAsync(int id)
        {
            return await _logInfoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<LogInfo>> GetManyLogsAsync(Expression<Func<LogInfo, bool>> filter)
        {
            return await _logInfoRepository.GetManyAsync(filter);
        }

        public async Task SaveLogsAsync()
        {
            await _logInfoRepository.SaveAsync();
        }

        public async Task UpdateLogAsync(LogInfo entity)
        {
            await _logInfoRepository.UpdateAsync(entity);
        }

    }
}
