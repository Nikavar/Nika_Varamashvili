using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IRoleUserService
    {
        Task<IEnumerable<RoleUser>> GetAllRoleUsersAsync();
        Task<IEnumerable<RoleUser>> GetManyRoleUsersAsync(Expression<Func<RoleUser, bool>> filter);
        Task<RoleUser> GetRoleUserByIdAsync(int id);
        Task<RoleUser> AddRoleUserAsync(RoleUser entity);
        Task UpdateRoleUserAsync(RoleUser entity);
        Task DeleteRoleUserAsync(RoleUser entity);
        Task DeleteManyRoleUsersAsync(Expression<Func<RoleUser, bool>> filter);
    }
    public class RoleUserService : IRoleUserService
    {
        private readonly IRoleUserRepository _roleUserRrepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleUserService(IRoleUserRepository roleUserRepository, IUnitOfWork unitOfWork)
        {
            _roleUserRrepository = roleUserRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<RoleUser> AddRoleUserAsync(RoleUser entity)
        {
            return await _roleUserRrepository.AddAsync(entity);
        }

        public async Task DeleteManyRoleUsersAsync(Expression<Func<RoleUser, bool>> filter)
        {
            await _roleUserRrepository.DeleteManyAsync(filter);
        }

        public async Task DeleteRoleUserAsync(RoleUser entity)
        {
            await _roleUserRrepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<RoleUser>> GetAllRoleUsersAsync()
        {
            return await _roleUserRrepository.GetAllAsync();
        }

        public async Task<IEnumerable<RoleUser>> GetManyRoleUsersAsync(Expression<Func<RoleUser, bool>> filter)
        {
            return await _roleUserRrepository.GetManyAsync(filter);
        }

        public async Task<RoleUser> GetRoleUserByIdAsync(int id)
        {
            return await _roleUserRrepository.GetByIdAsync(id);
        }

        public async Task UpdateRoleUserAsync(RoleUser entity)
        {
            await _roleUserRrepository.UpdateAsync(entity);
        }
    }

}
