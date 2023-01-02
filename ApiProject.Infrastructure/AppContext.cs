using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ApiProject.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure
{
    public class AppContext : DbContext
    {
        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
