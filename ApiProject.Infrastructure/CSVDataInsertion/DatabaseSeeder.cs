using ApiProject.Domain.Entities;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ApiProject.Infrastructure.CSVDataInsertion
{
    public static class DatabaseSeeder
    {
        public static  void Initialize(ApiAppContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Animes.Any())
            {
                using (var reader = new StreamReader(@"../anime.csv"))
                {
                    using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<AnimeMap>();
                        var records = csv.GetRecords<Anime>();
                        foreach (Anime? record in records)
                        {
                            context.Add(record);
                            context.SaveChanges();
                        }
                    }
                }
                
            }
        }
    }
}
