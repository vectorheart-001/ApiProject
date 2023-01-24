using ApiProject.Domain.DTOs.AnimeDTO;
using ApiProject.Domain.Entities;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.AnimeRepository
{

    public class AnimeRepository : IAnimeRepository
    {
        protected readonly ApiAppContext _context;
        public AnimeRepository(ApiAppContext context)
        {
            _context = context;
        }
       

        public async Task<Tuple<List<Anime>, int>> GetByGenre(string genre, int page)
        {
            var pageResult = 15f;
            var listQuery = _context.Animes.Where(x => x.Genre.ToLower().Contains(genre));
            var list = await listQuery.Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            var pageCount = Math.Ceiling(listQuery.Count() / pageResult);
            return Tuple.Create(list, (int)pageCount);
        }

        public async Task<Anime> GetById(Guid id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task<Tuple<List<Anime>, int>> GetByName(string name, int page)
        {
            var pageResult = 15f;
            var listQuery = _context.Animes.Where(x => x.Name.ToLower().Contains(name));
            var list = await listQuery.Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            var pageCount = Math.Ceiling(listQuery.Count() / pageResult);
            return Tuple.Create(list, (int)pageCount);
        }
        public async Task<Tuple<List<Anime>, int>> GetAll(int page )
        {
            var pageResult = 15f;
            var pageCount = Math.Ceiling(_context.Animes.Count()/ pageResult);
            List<Anime> animes= await _context.Animes
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            return Tuple.Create(animes, (int)pageCount);

        }

        public async Task Edit(string id, AnimeEditDTO anime)
        {
            var editAnime = await _context.Animes.FindAsync(id);
            editAnime.Name = anime.Name;
            editAnime.Genre = anime.Genre;
            editAnime.Episodes = anime.Episodes;
            editAnime.Type = anime.Type;
            editAnime.Rating = anime.Rating;
            editAnime.Members = anime.Members;
            _context.Update(editAnime);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Exists(string id)
        {
            return await _context.Animes.AnyAsync(x => x.Id == id);
        }
        public async Task<byte[]> PdfFileStats()
        {
            var query = _context.Animes;
            var totalAnime = query.Count();
            var animeByRomaanceGenre = query.Where(x => x.Genre.Contains("Romance")).Count();
            var animeByActionGenre = query.Where(x => x.Genre.Contains("Action")).Count();
            var animeByComedyGenre = query.Where(x => x.Genre.Contains("Comedy")).Count();
            var animeByDramaGenre = query.Where(x => x.Genre.Contains("Drama")).Count();
            var path = System.IO.Directory.GetCurrentDirectory() + @"\stats.pdf";
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Some stats about anime from us")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text($"Total amount of anime:{totalAnime}");
                            x.Item().Text($"Amount of action animes:{animeByActionGenre}");
                            x.Item().Text($"Amount of comedy animes:{animeByComedyGenre}");
                            x.Item().Text($"Amount of drama animes:{animeByDramaGenre}");
                            x.Item().Text($"Amount of romance animes:{animeByRomaanceGenre}");
                            x.Item().Image(Placeholders.Image(200, 100));
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf("stats.pdf");

            var byteFile = File.ReadAllBytesAsync(path);
            return await byteFile;
            


        }
    }
}
