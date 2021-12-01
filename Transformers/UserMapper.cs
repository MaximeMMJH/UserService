using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_service.Models;
using UserService.Models;

namespace User_service.Transformers
{
    public static class UserMapper
    {
        public static UserDbModel MapJsonToDb(UserJsonModel userJsonModel)
        {
            return new UserDbModel() {
                Id = userJsonModel.id,
                Username = userJsonModel.username,
                Email = userJsonModel.email
            };
        }
        public static UserJsonModel MapDbToJson(UserDbModel userDbModel)
        {
            return new UserJsonModel() {
                id = userDbModel.Id,
                username = userDbModel.Username,
                email = userDbModel.Email
            };
        }
        public static List<UserJsonModel> MapDbToJsonList(List<UserDbModel> userDbModels)
        {
            return userDbModels.Select(userModel => MapDbToJson(userModel)).ToList();
        }
        public static List<UserDbModel> MapJsonToDbList(List<UserJsonModel> userDbModels)
        {
            return userDbModels.Select(jsonModel => MapJsonToDb(jsonModel)).ToList();
        }
        public static UserDbModel MapBusToDb(UserCreationModel user)
        {
            return new UserDbModel()
            {
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
