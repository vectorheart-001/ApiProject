using ApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.UserRepository
{
    internal interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUserName(string name);
        Task Create(User user);
        Task<User> GetById(Guid id);
    }
}
