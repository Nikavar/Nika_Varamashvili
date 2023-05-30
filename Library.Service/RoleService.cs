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
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IEnumerable<Role>> GetManyRolesAsync(Expression<Func<Role, bool>> filter);
        Task<Role> GetRoleByIdAsync(int id);
        Task AddRoleAsync(Role entity);
        Task UpdateRoleAsync(Role entity);
        Task DeleteRoleAsync(Role entity);
        Task DeleteManyRolesAsync(Expression<Func<Role, bool>> filter);
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;


        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddRoleAsync(Role entity)
        {
            await _roleRepository.AddAsync(entity);
        }

        public async Task DeleteManyRolesAsync(Expression<Func<Role, bool>> filter)
        {
            await _roleRepository.DeleteManyAsync(filter);
        }

        public async Task DeleteRoleAsync(Role entity)
        {
            await _roleRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public Task<IEnumerable<Role>> GetManyRolesAsync(Expression<Func<Role, bool>> filter)
        {
            return _roleRepository.GetManyAsync(filter);
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task UpdateRoleAsync(Role entity)
        {
            await _roleRepository.UpdateAsync(entity);
        }
    }
}
