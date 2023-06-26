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
    public interface ITabService
    {
        Task<IEnumerable<Tab>> GetAllTabsAsync();
        Task<IEnumerable<Tab>> GetManyTabsAsync(Expression<Func<Tab, bool>> filter);
        Task<Tab> GetTabByIdAsync(int id);
        Task AddTabAsync(Tab entity);
        Task UpdateTabAsync(Tab entity);
        Task DeleteTabAsync(Tab user);
        Task DeleteManyTabsAsync(Expression<Func<Tab, bool>> filter);
        Task SaveTabAsync();
    }

    public class TabService : ITabService
    {
        private readonly ITabRepository _tabRepository;
        private readonly ILogInfoRepository _logInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TabService(ITabRepository tabRepo, ILogInfoRepository logRepo, IUnitOfWork unitOfWork)
        {
            _tabRepository = tabRepo;
            _logInfoRepository = logRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task AddTabAsync(Tab entity)
        {
            await _tabRepository.AddAsync(entity);
        }

        public async Task DeleteManyTabsAsync(Expression<Func<Tab, bool>> filter)
        {
            await _tabRepository.DeleteManyAsync(filter);
        }

        public Task DeleteTabAsync(Tab user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tab>> GetAllTabsAsync()
        {
            return await _tabRepository.GetAllAsync();
        }

        public Task<IEnumerable<Tab>> GetManyTabsAsync(Expression<Func<Tab, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Tab> GetTabByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveTabAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTabAsync(Tab entity)
        {
            throw new NotImplementedException();
        }
    }
}
