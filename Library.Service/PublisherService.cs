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
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<IEnumerable<Publisher>> GetManyPublishersAsync(Expression<Func<Publisher, bool>> filter);
        Task<Publisher> GetPublisherByIdAsync(int? id);
        Task AddPublisherAsync(Publisher entity);
        Task UpdatePublisherAsync(Publisher entity);
        Task DeletePublisherAsync(Publisher entity);
        Task DeleteManyPublishersAsync(Expression<Func<Publisher, bool>> filter);
        Task SavePublisherAsync();
    }

    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IPublisherRepository publisherRepo, IUnitOfWork unitOfWork)
        {
            _publisherRepository = publisherRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync() => await _publisherRepository.GetAllAsync();

        public async Task<IEnumerable<Publisher>> GetManyPublishersAsync(Expression<Func<Publisher, bool>> filter)
            => await _publisherRepository.GetManyAsync(filter);

        public async Task<Publisher> GetPublisherByIdAsync(int? id) => await _publisherRepository.GetByIdAsync(id);

        public async Task AddPublisherAsync(Publisher entity) => await _publisherRepository.AddAsync(entity);

        public async Task DeleteManyPublishersAsync(Expression<Func<Publisher, bool>> filter) => 
                                      await _publisherRepository.DeleteManyAsync(filter);

        public async Task DeletePublisherAsync(Publisher entity) => await _publisherRepository.DeleteAsync(entity);

        public async Task UpdatePublisherAsync(Publisher entity) => await _publisherRepository.UpdateAsync(entity);

        public async Task SavePublisherAsync() => await _publisherRepository.SaveAsync();
    }

}
