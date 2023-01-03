using ApiProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.UserRepository
{
    public class UserRepository:IUserRepository
    {
        protected readonly ApiAppContext _context;
        public UserRepository(ApiAppContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUserName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }


        async Task<User> IUserRepository.GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}

