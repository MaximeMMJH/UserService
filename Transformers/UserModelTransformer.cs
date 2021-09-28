using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_service.Models;

namespace User_service.Transformers
{
    public static class UserModelTransformer
    {
        public static UserDbModel transformToDbModel(UserJsonModel userJsonModel)
        {
            return new UserDbModel(
                userJsonModel.id,
                userJsonModel.firstname,
                userJsonModel.lastname,
                userJsonModel.email);
        }
        public static UserJsonModel transformToJsonModel(UserDbModel userDbModel)
        {
            return new UserJsonModel(
                userDbModel.id,
                userDbModel.firstname,
                userDbModel.lastname,
                userDbModel.email);
        }
        public static List<UserJsonModel> transformToJsonModels(List<UserDbModel> userDbModels)
        {
            return userDbModels.Select(userModel => transformToJsonModel(userModel)).ToList();
        }
        public static List<UserDbModel> transformToDbModels(List<UserJsonModel> userDbModels)
        {
            return userDbModels.Select(jsonModel => transformToDbModel(jsonModel)).ToList();
        }
    }
}
