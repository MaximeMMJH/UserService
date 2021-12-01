using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_service.Models;
using User_service.Repositories;

namespace User_service.Facades
{
    public class UserFacade
    {
        private UserRepository repository;

        public UserFacade(UserRepository userRepository)
        {
            repository = userRepository;
        }

        internal List<UserDbModel> GetUsers()
        {
            return repository.GetUsers();
        }

        internal UserDbModel CreateUser(UserDbModel userDbModel)
        {
            return repository.CreateUser(userDbModel);
        }

        internal UserDbModel GetUser(Guid id)
        {
            return repository.GetUser(id);
        }

        internal void DeleteUser(Guid id)
        {
            repository.DeleteUser(id);
        }

        internal UserDbModel UpdateUser(UserDbModel userDbModel)
        {
            return repository.UpdateUser(userDbModel);
        }
    }
}
