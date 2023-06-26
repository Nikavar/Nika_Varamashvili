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
        public async Task<IEnumerable<Category>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Category>> GetManyAuthorsAsync(Expression<Func<Category, bool>> filter)
        {
            return await _authorRepository.GetManyAsync(filter);
        }

        public async Task<Category> GetAuthorByIdAsync(params object[] key)
        {
            return await _authorRepository.GetByIdAsync(key);
        }

        public async Task<Category> AddAuthorAsync(Category entity)
        {
            return await _authorRepository.AddAsync(entity);    
        }

        public async Task DeleteAuthorAsync(Category entity)
        {
            await _authorRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyAuthorsAsync(Expression<Func<Category, bool>> filter)
        {
            await _authorRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateAuthorAsync(Category entity)
        {
            await _authorRepository.UpdateAsync(entity);
        }
    }

    public interface IAuthorService
    {
        Task<IEnumerable<Category>> GetAllAuthorsAsync();
        Task<IEnumerable<Category>> GetManyAuthorsAsync(Expression<Func<Category, bool>> filter);
        Task<Category> GetAuthorByIdAsync(params object[] key);
        Task<Category> AddAuthorAsync(Category entity);
        Task UpdateAuthorAsync(Category entity);
        Task DeleteAuthorAsync(Category entity);
        Task DeleteManyAuthorsAsync(Expression<Func<Category, bool>> filter);
    }
}
