﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodMoodProvider.ViewsModels;
using GoodMoodProvider.Models;

namespace GoodMoodProvider.DataContexts

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<WordRating> WordsRating {get; set;}
        public DbSet<UserRole> UserRoles { get; set; }


    }
}
