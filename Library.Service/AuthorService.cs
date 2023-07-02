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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IAuthorRepository authorRepo, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Author>> GetManyAuthorsAsync(Expression<Func<Author, bool>> filter)
        {
            return await _authorRepository.GetManyAsync(filter);
        }

        public async Task<Author> GetAuthorByIdAsync(params object[] key)
        {
            return await _authorRepository.GetByIdAsync(key);
        }

        public async Task<Author> AddAuthorAsync(Author entity)
        {
            return await _authorRepository.AddAsync(entity);    
        }

        public async Task DeleteAuthorAsync(Author entity)
        {
            await _authorRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyAuthorsAsync(Expression<Func<Author, bool>> filter)
        {
            await _authorRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateAuthorAsync(Author entity)
        {
            await _authorRepository.UpdateAsync(entity);
        }
    }

    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<IEnumerable<Author>> GetManyAuthorsAsync(Expression<Func<Author, bool>> filter);
        Task<Author> GetAuthorByIdAsync(params object[] key);
        Task<Author> AddAuthorAsync(Author entity);
        Task UpdateAuthorAsync(Author entity);
        Task DeleteAuthorAsync(Author entity);
        Task DeleteManyAuthorsAsync(Expression<Func<Author, bool>> filter);
    }
}
