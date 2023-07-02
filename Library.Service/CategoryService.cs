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
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository categoryRepo, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Category>> GetManyCategoriesAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryRepository.GetManyAsync(filter);
        }

        public async Task<Category> GetCategoryByIdAsync(params object[] key)
        {
            return await _categoryRepository.GetByIdAsync(key);
        }

        public Task<Category> AddCategoryAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyCategoriesAsync(Expression<Func<Category, bool>> filter)
        {
            throw new NotImplementedException();
        }


        public Task UpdateCategoryAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetManyCategoriesAsync(Expression<Func<Category, bool>> filter);
        Task<Category> GetCategoryByIdAsync(params object[] key);
        Task<Category> AddCategoryAsync(Category entity);
        Task UpdateCategoryAsync(Category entity);
        Task DeleteCategoryAsync(Category entity);
        Task DeleteManyCategoriesAsync(Expression<Func<Category, bool>> filter);
    }
}
