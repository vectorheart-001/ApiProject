using System;
using ApiProject.Domain.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Domain.Entities
{
    public sealed class Anime
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Type { get; set; }
        public string Episodes { get; set; }
        public float Rating { get; set; }
        public int Members { get; set; }
    }
}
