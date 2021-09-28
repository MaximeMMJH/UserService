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
            return new UserDbModel(Guid.NewGuid(), "", "", "");
        }

        internal void deleteUser(Guid id)
        {

        }

        internal UserDbModel UpdateUser(UserDbModel userDbModel)
        {
            return new UserDbModel(Guid.NewGuid(), "", "", "");
        }
    }
}
