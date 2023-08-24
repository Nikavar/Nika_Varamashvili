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
    public class BookPublisherService : IBookPublisherService
	{
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookPublisherService(IBookPublisherRepository bookPublisherRepo, IUnitOfWork unitOfWork)
        {
			_bookPublisherRepository = bookPublisherRepo;
            this.unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<BookPublisher>> GetAllBookPublishersAsync()
		{
			return await _bookPublisherRepository.GetAllAsync();
		}
		public async Task<IEnumerable<BookPublisher>> GetManyBookPublishersAsync(Expression<Func<BookPublisher, bool>> filter)
		{
			return await _bookPublisherRepository.GetManyAsync(filter);
		}

		public async Task<BookPublisher> GetBookPublisherByIdAsync(params object[] key)
		{
			return await _bookPublisherRepository.GetByIdAsync(key);
		}

		public async Task<BookPublisher> AddBookPublisherAsync(BookPublisher entity)
		{
			return await _bookPublisherRepository.AddAsync(entity);
		}

		public async Task DeleteBookPublisherAsync(BookPublisher entity)
		{
			await _bookPublisherRepository.DeleteAsync(entity);
		}

		public async Task DeleteManyBookPublishersAsync(Expression<Func<BookPublisher, bool>> filter)
		{
			await _bookPublisherRepository.DeleteManyAsync(filter);
		}

		public async Task UpdateBookPublisherAsync(BookPublisher entity)
		{
			await _bookPublisherRepository.UpdateAsync(entity);	
		}
	}

    public interface IBookPublisherService
	{
        Task<IEnumerable<BookPublisher>> GetAllBookPublishersAsync();
        Task<IEnumerable<BookPublisher>> GetManyBookPublishersAsync(Expression<Func<BookPublisher, bool>> filter);
        Task<BookPublisher> GetBookPublisherByIdAsync(params object[] key);
        Task<BookPublisher> AddBookPublisherAsync(BookPublisher entity);
        Task UpdateBookPublisherAsync(BookPublisher entity);
        Task DeleteBookPublisherAsync(BookPublisher entity);
        Task DeleteManyBookPublishersAsync(Expression<Func<BookPublisher, bool>> filter);
    }
}
