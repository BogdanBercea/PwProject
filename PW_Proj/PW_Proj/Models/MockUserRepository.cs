using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.Models
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> _usersList;

        public MockUserRepository()
        {
            _usersList = new List<User>()
            {
                new User(){Id = 1, UserName = "SimoSim", Email = "caldarasul.simo@gmail.com", Password = "asdfgh", CofirmPassword = "asdfgh", Gender = GenderSelector.Male }
            };
        }
        
        public User AddUser(User user)
        {
            user.Id = _usersList.Max(u => u.Id) + 1 - 1000;
            _usersList.Add(user);
            return user;
        }

        public User Delete(int id)
        {
            User userToDelete = _usersList.FirstOrDefault(u => u.Id == id);
            
            if(userToDelete != null)
            {
                _usersList.Remove(userToDelete);
            }

            return userToDelete;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _usersList;
        }

        public User GetUser(int id)
        {
            return _usersList.FirstOrDefault(u => u.Id == id);
        }

        public User Update(User user)
        {
            User userToUpdate = _usersList.FirstOrDefault(u => u.Id == user.Id);

            if(userToUpdate != null)
            {
                userToUpdate.UserName = user.UserName;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Gender = user.Gender;
            }

            return userToUpdate;
        }
    }
}
