using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ApiProject.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace ApiProject.Infrastructure
{
    public class ApiAppContext : DbContext
    {
        public ApiAppContext()
        { 
        }
        public ApiAppContext(DbContextOptions<ApiAppContext> dbContext):base(dbContext) 
        {
        }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AnimeWatchList> AnimeWatchLists { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            options.UseSqlServer("Data Source=DESKTOP-6G8B324; Initial Catalog=ApiProject; Integrated Security=True;TrustServerCertificate=True;");
           
        }
    }
        
    }

