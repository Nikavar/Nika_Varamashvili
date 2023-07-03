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
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository storageRepository;
        private readonly IUnitOfWork unitOfWork;

        public StorageService(IStorageRepository storageRepo, IUnitOfWork unitOfWork)
        {
            this.storageRepository = storageRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Storage>> GetAllStoragesAsync()
        {
            return await storageRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Storage>> GetManyStoragesAsync(Expression<Func<Storage, bool>> filter)
        {
            return await storageRepository.GetManyAsync(filter);
        }

        public async Task<Storage> GetStorageByIdAsync(int? id)
        {
            return await storageRepository.GetByIdAsync(id);
        }
        public async Task<Storage> AddStorageAsync(Storage entity)
        {
            return await storageRepository.AddAsync(entity);    
        }
        public Task UpdateStorageAsync(Storage entity)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteManyStoragesAsync(Expression<Func<Storage, bool>> filter)
        {
            await storageRepository.DeleteManyAsync(filter);    
        }

        public async Task DeleteStorageAsync(Storage entity)
        {
            await storageRepository.DeleteAsync(entity);
        }

        public async Task SaveStorageAsync()
        {
            await storageRepository.SaveAsync();
        }
    }

    public interface IStorageService
    {
        Task<IEnumerable<Storage>> GetAllStoragesAsync();
        Task<IEnumerable<Storage>> GetManyStoragesAsync(Expression<Func<Storage, bool>> filter);
        Task<Storage> GetStorageByIdAsync(int? id);
        Task<Storage> AddStorageAsync(Storage entity);
        Task UpdateStorageAsync(Storage entity);
        Task DeleteStorageAsync(Storage entity);
        Task DeleteManyStoragesAsync(Expression<Func<Storage, bool>> filter);
        Task SaveStorageAsync();
    }
}
