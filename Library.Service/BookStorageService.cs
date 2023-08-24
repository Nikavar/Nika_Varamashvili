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
    public class BookStorageService : IBookStorageService
    {
        private readonly IBookStorageRepository _bookStorageRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookStorageService(IBookStorageRepository bookStorageRepo, IUnitOfWork unitOfWork)
        {
            _bookStorageRepository = bookStorageRepo;
            this.unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<BookStorage>> GetAllBookStoragesAsync()
		{
			return await _bookStorageRepository.GetAllAsync();
		}

		public async Task<BookStorage> GetBookStorageByIdAsync(params object[] key)
		{
			return await _bookStorageRepository.GetByIdAsync(key);
		}

		public async Task<IEnumerable<BookStorage>> GetManyBookStoragesAsync(Expression<Func<BookStorage, bool>> filter)
		{
			return await _bookStorageRepository.GetManyAsync(filter);
		}

		public async Task<BookStorage> AddBookStorageAsync(BookStorage entity)
		{
			return await _bookStorageRepository.AddAsync(entity);
		}

		public async Task DeleteBookStorageAsync(BookStorage entity)
		{
			await _bookStorageRepository.DeleteAsync(entity);
		}

		public async Task DeleteManyBookStoragesAsync(Expression<Func<BookStorage, bool>> filter)
		{
			await _bookStorageRepository.DeleteManyAsync(filter);
		}

		public async Task UpdateBookStorageAsync(BookStorage entity)
		{
			await _bookStorageRepository.UpdateAsync(entity);
		}
	}

    public interface IBookStorageService
	{
        Task<IEnumerable<BookStorage>> GetAllBookStoragesAsync();
        Task<IEnumerable<BookStorage>> GetManyBookStoragesAsync(Expression<Func<BookStorage, bool>> filter);
        Task<BookStorage> GetBookStorageByIdAsync(params object[] key);
        Task<BookStorage> AddBookStorageAsync(BookStorage entity);
        Task UpdateBookStorageAsync(BookStorage entity);
        Task DeleteBookStorageAsync(BookStorage entity);
        Task DeleteManyBookStoragesAsync(Expression<Func<BookStorage, bool>> filter);
    }
}
