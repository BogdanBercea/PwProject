using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.Models
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        
        public SqlUserRepository(AppDbContext context)
        {
            this._context = context;
        }
        
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Delete(int id)
        {
            User userToDelete = _context.Users.Find(id);

            if(userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }

            return userToDelete;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public User Update(User user)
        {
            var userToUpadate = _context.Users.Attach(user);
            userToUpadate.State = EntityState.Modified;
            _context.SaveChanges();

            return user;
        }
    }
}
