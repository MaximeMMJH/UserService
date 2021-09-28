using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User_service.Models
{
    public class UserDbModel
    {
        public UserDbModel(Guid id, string firstname, string lastname, string email)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
        }

        public Guid id { get; }
        public string firstname { get; }
        public string lastname { get; }
        public string email { get; }
    }
}
