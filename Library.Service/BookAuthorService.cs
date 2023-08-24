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
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookAuthorService(IBookAuthorRepository bookAuthorRepo, IUnitOfWork unitOfWork)
        {
            _bookAuthorRepository = bookAuthorRepo;
            this.unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<BookAuthor>> GetAllBookAuthorsAsync()
		{
			return await _bookAuthorRepository.GetAllAsync();
		}

		public async Task<BookAuthor> GetBookAuthorByIdAsync(params object[] key)
		{
			return await _bookAuthorRepository.GetByIdAsync(key);
		}

		public async Task<IEnumerable<BookAuthor>> GetManyBookAuthorsAsync(Expression<Func<BookAuthor, bool>> filter)
		{
			return await _bookAuthorRepository.GetManyAsync(filter);
		}

		public async Task<BookAuthor> AddBookAuthorAsync(BookAuthor entity)
		{
			return await _bookAuthorRepository.AddAsync(entity);
		}

		public async Task DeleteBookAuthorAsync(BookAuthor entity)
		{
			await _bookAuthorRepository.DeleteAsync(entity);
		}

		public async Task DeleteManyBookAuthorsAsync(Expression<Func<BookAuthor, bool>> filter)
		{
			await _bookAuthorRepository.DeleteManyAsync(filter);
		}

		public async Task UpdateBookAuthorAsync(BookAuthor entity)
		{
			await _bookAuthorRepository.UpdateAsync(entity);
		}
	}

    public interface IBookAuthorService
	{
        Task<IEnumerable<BookAuthor>> GetAllBookAuthorsAsync();
        Task<IEnumerable<BookAuthor>> GetManyBookAuthorsAsync(Expression<Func<BookAuthor, bool>> filter);
        Task<BookAuthor> GetBookAuthorByIdAsync(params object[] key);
        Task<BookAuthor> AddBookAuthorAsync(BookAuthor entity);
        Task UpdateBookAuthorAsync(BookAuthor entity);
        Task DeleteBookAuthorAsync(BookAuthor entity);
        Task DeleteManyBookAuthorsAsync(Expression<Func<BookAuthor, bool>> filter);
    }
}
