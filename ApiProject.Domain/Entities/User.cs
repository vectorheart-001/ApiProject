﻿using System;
using System.Collections.Generic;
using ApiProject.Domain.Primitives;
using System.Linq;
using ApiProject.Domain.Enums;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Domain.Entities
{
    
    public sealed class User:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles.Role Role { get; set; }
    }
}
