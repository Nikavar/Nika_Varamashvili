using Library.Model.Models;
using Library.Data.Infrastructure;
using Library.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetStaffReaderUser(int staffReaderId, string userName = null);
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void SaveUser();

    }
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        public readonly IStaffReaderRepository staffReaderRepository;
        public readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepo, IStaffReaderRepository staffReaderRepo, 
                           IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepo;
            this.staffReaderRepository = staffReaderRepo;
            this.unitOfWork = unitOfWork;
        }

        // added new method for auth of logged users
        public async Task<User> LoginUser(string userName, string password)
        {
            return await userRepository.AuthenticateUser(userName, password);
        }

        public void CreateUser(User user)
        {
            userRepository.Add(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            var users = userRepository.GetAll();           
            return users;
        }

        public IEnumerable<User> GetStaffReaderUser(int staffReaderId, string userName = null)
        {
            var staffReader = staffReaderRepository.GetById(staffReaderId);
            return staffReader.Users.Where(u => u.UserName.ToLower().Contains(userName.ToLower().Trim()));
        }

        public User GetUserById(int id)
        {
            var user = userRepository.GetById(id);
            return user;
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }
    }
}
