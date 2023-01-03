using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Domain.DTOs.AnimeDTO
{
    public class AnimeEditDTO
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Type { get; set; }
        public string Episodes { get; set; }
        public float Rating { get; set; }
        public int Members { get; set; }
       
    }
}
