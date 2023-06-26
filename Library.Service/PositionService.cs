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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork unitOfWork;

        public PositionService(IPositionRepository positionRepo, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Position> AddPositionAsync(Position entity)
        {
            await _positionRepository.AddAsync(entity);
            await _positionRepository.SaveAsync();

            return entity;
        }

        public async Task DeleteManyPositionsAsync(Expression<Func<Position, bool>> filter)
        {
            await _positionRepository.DeleteManyAsync(filter);
        }

        public async Task DeletePositionAsync(Position entity)
        {
            await _positionRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await _positionRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Position>> GetManyPositionsAsync(Expression<Func<Position, bool>> filter)
        {
            return await _positionRepository.GetManyAsync(filter);
        }

        public async Task<Position> GetPositionByIdAsync(params object[] key)
        {
            return await _positionRepository.GetByIdAsync(key);
        }

        public async Task UpdatePositionAsync(Position entity)
        {
            await _positionRepository.UpdateAsync(entity);
        }
    }


    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<IEnumerable<Position>> GetManyPositionsAsync(Expression<Func<Position, bool>> filter);
        Task<Position> GetPositionByIdAsync(params object[] key);
        Task<Position> AddPositionAsync(Position entity);
        Task UpdatePositionAsync(Position entity);
        Task DeletePositionAsync(Position user);
        Task DeleteManyPositionsAsync(Expression<Func<Position, bool>> filter);
    }
}
