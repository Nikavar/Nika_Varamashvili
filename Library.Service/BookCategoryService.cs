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
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepo, IUnitOfWork unitOfWork)
        {
            _bookCategoryRepository = bookCategoryRepo;
            this.unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<BookCategory>> GetAllBookCategoriesAsync()
		{
			return await _bookCategoryRepository.GetAllAsync();
		}
		public async Task<IEnumerable<BookCategory>> GetManyBookCategoriesAsync(Expression<Func<BookCategory, bool>> filter)
		{
			return await _bookCategoryRepository.GetManyAsync(filter);
		}

		public async Task<BookCategory> GetBookCategoryByIdAsync(params object[] key)
		{
			return await _bookCategoryRepository.GetByIdAsync(key);
		}

		public async Task<BookCategory> AddBookCategoryAsync(BookCategory entity)
		{
			return await _bookCategoryRepository.AddAsync(entity);
		}

		public async Task DeleteBookCategoryAsync(BookCategory entity)
		{
			await _bookCategoryRepository.DeleteAsync(entity);
		}

		public async Task DeleteManyBookCategoriesAsync(Expression<Func<BookCategory, bool>> filter)
		{
			await _bookCategoryRepository.DeleteManyAsync(filter);
		}

		public async Task UpdateBookCategoryAsync(BookCategory entity)
		{
			await _bookCategoryRepository.UpdateAsync(entity);
		}
	}

    public interface IBookCategoryService
	{
        Task<IEnumerable<BookCategory>> GetAllBookCategoriesAsync();
        Task<IEnumerable<BookCategory>> GetManyBookCategoriesAsync(Expression<Func<BookCategory, bool>> filter);
        Task<BookCategory> GetBookCategoryByIdAsync(params object[] key);
        Task<BookCategory> AddBookCategoryAsync(BookCategory entity);
        Task UpdateBookCategoryAsync(BookCategory entity);
        Task DeleteBookCategoryAsync(BookCategory entity);
        Task DeleteManyBookCategoriesAsync(Expression<Func<BookCategory, bool>> filter);
    }
}
