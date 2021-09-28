using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_service.Models;

namespace User_service.Facades
{
    public class UserFacade
    {
        private List<UserDbModel> users;

        public UserFacade()
        {
            users = new List<UserDbModel>();
        }

        internal List<UserDbModel> GetUsers()
        {
            return users;
        }

        internal UserDbModel CreateUser(UserDbModel userDbModel)
        {
            users.Add(userDbModel);
            return userDbModel;
        }

        internal UserDbModel getUser(Guid id)
        {
            return users.FirstOrDefault(user => user.id.Equals(id));
        }

        internal void deleteUser(Guid id)
        {
            users.Remove(getUser(id));
        }

        internal UserDbModel UpdateUser(UserDbModel userDbModel)
        {
            deleteUser(userDbModel.id);
            CreateUser(userDbModel);
            return userDbModel;
        }
    }
}
