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
    public interface IStaffReaderService
    {
        Task<IEnumerable<StaffReader>> GetAllStaffReadersAsync();
        Task<IEnumerable<StaffReader>> GetManyStaffReadersAsync(Expression<Func<StaffReader, bool>> filter);
        Task<StaffReader> GetStaffReaderByIdAsync(int id);
        Task AddStaffReaderAsync(StaffReader entity);
        Task UpdateStaffReaderAsync(StaffReader entity);
        Task DeleteStaffReaderAsync(StaffReader user);
        Task DeleteManyStaffReadersAsync(Expression<Func<StaffReader, bool>> filter);

    }

    public class StaffReaderService : IStaffReaderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStaffReaderRepository _staffReaderRepository;
        private readonly IUnitOfWork unitOfWork;

        public StaffReaderService(IUserRepository userRepository, IStaffReaderRepository staffReader, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._staffReaderRepository = staffReader;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddStaffReaderAsync(StaffReader entity)
        {
            await _staffReaderRepository.AddAsync(entity);
        }

        public async Task DeleteManyStaffReadersAsync(Expression<Func<StaffReader, bool>> filter)
        {
            await _staffReaderRepository.DeleteManyAsync(filter);
        }

        public async Task DeleteStaffReaderAsync(StaffReader user)
        {
            await _staffReaderRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<StaffReader>> GetAllStaffReadersAsync()
        {
            return await _staffReaderRepository.GetAllAsync();
        }

        public Task<IEnumerable<StaffReader>> GetManyStaffReadersAsync(Expression<Func<StaffReader, bool>> filter)
        {
            return _staffReaderRepository.GetManyAsync(filter);
        }

        public async Task<StaffReader> GetStaffReaderByIdAsync(int id)
        {
            return await _staffReaderRepository.GetByIdAsync(id);
        }

        public async Task UpdateStaffReaderAsync(StaffReader entity)
        {
            await _staffReaderRepository.UpdateAsync(entity);
        }
    }
}
