using ApiProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.AnimeWishListRepository
{
    public class AnimeWatchListRepository : IAnimeWatchListRepository
    {
        protected readonly ApiAppContext _context;
        public AnimeWatchListRepository(ApiAppContext context)
        {
            _context = context;
        }
        public async Task AddToList(Guid userId, string animeId)
        {
            await _context.AnimeWatchLists.AddAsync(new AnimeWatchList()
            {
                UserId = userId,
                AnimeId = animeId,
                IsWatched = false,
            });
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsWatched(string animeId)
        {
            var anime = _context.AnimeWatchLists.FirstOrDefault(x => x.AnimeId == animeId);
            anime.IsWatched ^= true;
            _context.Update(anime);
            _context.SaveChangesAsync();
        }

        public async Task RemoveFromList(Guid userId,string animeId)
        {
            var anime = await _context.AnimeWatchLists.Where(x => x.AnimeId == animeId && x.UserId == userId).ToListAsync();
            _context.Remove(anime);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AnimeWatchList>> ViewList(Guid userId)
        {
            return await _context.AnimeWatchLists.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
