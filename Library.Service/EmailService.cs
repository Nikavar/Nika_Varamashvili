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
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository emailRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmailService(IEmailRepository emailRepo, IUnitOfWork unitOfWork)
        {
            this.emailRepository = emailRepo;
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return emailRepository.GetAllAsync();
        }

        public Task<IEnumerable<Email>> GetManyEmailsAsync(Expression<Func<Email, bool>> filter)
        {
            return emailRepository.GetManyAsync(filter);
        }

        public async Task<Email> GetEmailByIdAsync(params object[] key)
        {
            return await emailRepository.GetByIdAsync(key);
        }
        public async Task<Email> AddEmailAsync(Email entity)
        {
            return await emailRepository.AddAsync(entity);
        }

        public async Task DeleteEmailAsync(Email entity)
        {
            await emailRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyEmailsAsync(Expression<Func<Email, bool>> filter)
        {
            await emailRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateEmailAsync(Email entity)
        {
            await emailRepository.UpdateAsync(entity);  
        }

        //public IEnumerable<Email> GetEmailEntityByTemplateTypeAsync(Expression<Func<Email,bool>>filter)
        //{
        //    return emailRepository.GetEmailEntityByTemplateTypeAsync(filter);
        //}
    }

    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetAllEmailsAsync();
        Task<IEnumerable<Email>> GetManyEmailsAsync(Expression<Func<Email, bool>> filter);
        Task<Email> GetEmailByIdAsync(params object[] key);
        Task<Email> AddEmailAsync(Email entity);
        Task UpdateEmailAsync(Email entity);
        Task DeleteEmailAsync(Email entity);
        Task DeleteManyEmailsAsync(Expression<Func<Email, bool>> filter);
        //Task<IEnumerable<Email>> GetEmailEntityByTemplateTypeAsync(Expression<Func<Email,bool>> filter);
    }
}
