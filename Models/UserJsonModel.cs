using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace User_service.Models
{
    public class UserJsonModel
    {
        public Guid id { get; set;  }
        public string username { get; set; }
        public string email { get; set; }
    }
}
