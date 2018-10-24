using Microsoft.EntityFrameworkCore;
using RayBlog.Core.Entities;
using RayBlog.Infrastructure.EntiyConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayBlog.Infrastructure.Database
{
    public  class MyContext:DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
        public DbSet<Post> Posts { get; set; }
    }
}
