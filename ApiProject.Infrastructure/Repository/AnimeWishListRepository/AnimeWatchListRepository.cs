using ApiProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;


namespace ApiProject.Infrastructure.Repository.AnimeWishListRepository
{
    public class AnimeWatchListRepository : IAnimeWatchListRepository
    {
        protected readonly ApiAppContext _context;
        protected readonly IMemoryCache _memoryCache;

        public AnimeWatchListRepository(IMemoryCache memoryCache,ApiAppContext context)
        {
            _memoryCache = memoryCache;
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

        public async Task<bool> is_OnList(Guid userId, string animeId)
        {
           var check = _context.AnimeWatchLists.Any(x => x.UserId == userId && x.AnimeId == animeId);
           return check;
        }

        public async Task MarkAsWatched(string animeId,Guid userId)
        {
            var anime = _context.AnimeWatchLists.FirstOrDefault(x => x.AnimeId == animeId && x.UserId == userId);
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
            List<AnimeWatchList> list;
            list = _memoryCache.Get<List<AnimeWatchList>>($"anime-watchlist{userId}");
            if(list == null)
            {
                list = new();
                list = await _context.AnimeWatchLists.Include(x => x.Anime)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
                _memoryCache.Set($"anime-watchlist{userId}", list,TimeSpan.FromHours(3));
            }
            return list;
        }
    }
}
