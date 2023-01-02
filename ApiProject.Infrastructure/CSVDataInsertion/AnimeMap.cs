using ApiProject.Domain.Entities;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.CSVDataInsertion
{
    internal class AnimeMap:ClassMap<Anime>
    {
        public AnimeMap()
        {
           
            Map(a => a.Id).Name("anime_id");
            Map(a => a.Name).Name("name").Default("Unknown");
            Map(a => a.Genre).Name("genre").Default("No known genre");
            Map(a => a.Type).Name("type").Default("Unknown tye");
            Map(a => a.Episodes).Name("episodes");
            Map(a => a.Rating).Name("rating").Default(0);
            Map(a => a.Members).Name("members").Default("None");
        }
    }
}
