using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using GoodMoodProvider.Models;

namespace GoodMoodProvider.DataContexts

{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> ID { get; set; }
        public DbSet<User> Nickname { get; set; }
        public DbSet<User> FirstName { get; set; }
        public DbSet<User> SecondName { get; set; }
        public DbSet<User> Age { get; set; }
        public DbSet<User> Gender { get; set; }
        public DbSet<User> RegDate { get; set; }
        public DbSet<User> IsOnline { get; set; }


    }
}
