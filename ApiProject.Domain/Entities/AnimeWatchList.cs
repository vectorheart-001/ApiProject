using ApiProject.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Domain.Entities
{
    public class AnimeWatchList:BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string AnimeId { get; set; }
        public virtual Anime Anime { get; set; }
        public bool IsWatched { get; set; }
    }
}
