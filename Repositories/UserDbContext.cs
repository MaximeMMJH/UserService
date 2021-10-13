using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User_service.Models;

namespace UserService.Repositories
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) :base(options)
        {

        }

        public DbSet<UserDbModel> Users { get; set; }
    }
}
