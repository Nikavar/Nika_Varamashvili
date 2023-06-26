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
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
        Task<IEnumerable<Language>> GetManyLanguagesAsync(Expression<Func<Language, bool>> filter);
        Task<Language> GetLanguageByIdAsync(params object[] key);
        Task<Language> AddLanguageAsync(Language entity); 
        Task UpdateLanguageAsync(Language entity);
        Task DeleteLanguageAsync(Language user);
        Task DeleteManyLanguagesAsync(Expression<Func<Language, bool>> filter);
    }
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogInfoRepository _logInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LanguageService(ILanguageRepository languageRepo, ILogInfoRepository logRepo, IUnitOfWork unitOfWork)
        {
            _languageRepository = languageRepo;
            _logInfoRepository = logRepo;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _languageRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Language>> GetManyLanguagesAsync(Expression<Func<Language, bool>> filter)
        {
            return await _languageRepository.GetManyAsync(filter);
        }

        public async Task<Language> GetLanguageByIdAsync(params object[] key)
        {
            return await _languageRepository.GetByIdAsync(key);
        }


        public async Task<Language> AddLanguageAsync(Language entity)
        {
            return await _languageRepository.AddAsync(entity);
        }

        public async Task DeleteLanguageAsync(Language user)
        {
            await _languageRepository.DeleteAsync(user);
        }

        public async Task DeleteManyLanguagesAsync(Expression<Func<Language, bool>> filter)
        {
            await _languageRepository.DeleteManyAsync(filter);
        }


        public async Task UpdateLanguageAsync(Language entity)
        {
            await _languageRepository.UpdateAsync(entity);
        }
    }
}
