using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace User_service.Models
{
    [Table("Users")]
    public class UserJsonModel
    {
        public UserJsonModel(Guid id, string firstname, string lastname, string email)
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
