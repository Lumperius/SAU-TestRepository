using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodMoodProvider.ViesModels;


namespace GoodMoodProvider.DataContexts

{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> UserSet { get; set; }
        public DbSet<User> Nickname { get; set; }


    }
}
