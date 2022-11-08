using System;
using ApiProject.Domain.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Domain.Entities
{
    public sealed class Anime:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genres { get; set; }
    }
}
