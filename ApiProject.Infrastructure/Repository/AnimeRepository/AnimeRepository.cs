using ApiProject.Domain.Entities;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
       

        public async Task<Tuple<List<Anime>, string>> GetByGenre(string genre, int page =1)
        {
            var pageResult = 15f;
            var pageCount = Math.Ceiling(_context.Animes.Count() / pageResult);
            var list = await _context.Animes.Where(x => x.Name.ToLower().Contains(genre)).Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            return Tuple.Create(list, $"{page}/{pageCount}");
        }

        public async Task<Anime> GetById(Guid id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task<Tuple<List<Anime>, string>> GetByName(string name, int page = 1)
        {
            var pageResult = 15f;
            var pageCount = Math.Ceiling(_context.Animes.Count() / pageResult);
            var list = await _context.Animes.Where(x => x.Name.ToLower().Contains(name)).Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            return Tuple.Create(list, $"{page}/{pageCount}");
        }
        public async Task<Tuple<List<Anime>, string>> GetAll(int page = 1)
        {
            var pageResult = 15f;
            var pageCount = Math.Ceiling(_context.Animes.Count()/ pageResult);
            List<Anime> animes= await _context.Animes
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();
            return Tuple.Create(animes, $"{page}/{(int)pageCount}");

        }

        public async Task Edit(string id, Anime anime)
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
    }
}
