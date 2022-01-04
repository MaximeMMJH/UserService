using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_service.Models;
using UserService.Repositories;

namespace User_service.Repositories
{
    public class UserRepository
    {
        private UserDbContext userContext;

        public UserRepository(UserDbContext userContext)
        {
            this.userContext = userContext;
        }

        public UserDbModel CreateUser(UserDbModel user)
        {
            EntityEntry<UserDbModel> addedUser = userContext.Users.Add(user);
            userContext.SaveChanges();
            return addedUser.Entity;
        }

        public List<UserDbModel> GetUsers()
        {
            return userContext.Users.ToList();
        }

        public UserDbModel GetUser(Guid id)
        {
            return userContext.Users.Find(id);
        }

        public void DeleteUser(Guid id)
        {
            userContext.Remove(id);
            userContext.SaveChanges();
        }

        public UserDbModel UpdateUser(UserDbModel user)
        {
            EntityEntry<UserDbModel> updatedUser = userContext.Users.Update(user);
            userContext.SaveChanges();
            return updatedUser.Entity;
        }

        internal UserDbModel GetUserByAuthId(Guid id)
        {
            return userContext.Users.First(x => x.subId.Equals(id));

        }
    }
}
