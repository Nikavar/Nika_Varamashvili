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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookService(IBookRepository bookRepo, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Book>> GetManyBooksAsync(Expression<Func<Book, bool>> filter)
        {
            return await _bookRepository.GetManyAsync(filter);
        }

        public async Task<Book> GetBookByIdAsync(params object[] key)
        {
           return await _bookRepository.GetByIdAsync(key);
        }
        public async Task<Book> AddBookAsync(Book entity)
        {
            return await _bookRepository.AddAsync(entity);
        }

        public async Task DeleteBookAsync(Book entity)
        {
            await _bookRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyBooksAsync(Expression<Func<Book, bool>> filter)
        {
            await _bookRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateBookAsync(Book entity)
        {
            await _bookRepository.UpdateAsync(entity);
        }
    }

    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetManyBooksAsync(Expression<Func<Book, bool>> filter);
        Task<Book> GetBookByIdAsync(params object[] key);
        Task<Book> AddBookAsync(Book entity);
        Task UpdateBookAsync(Book entity);
        Task DeleteBookAsync(Book entity);
        Task DeleteManyBooksAsync(Expression<Func<Book, bool>> filter);
    }
}
