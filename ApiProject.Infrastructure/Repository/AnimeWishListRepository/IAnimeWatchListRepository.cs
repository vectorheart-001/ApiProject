using ApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.AnimeWishListRepository
{
    public interface IAnimeWatchListRepository
    {
        Task AddToList(Guid userId,string animeId);
        Task MarkAsWatched(string animeId,Guid userId);
        Task RemoveFromList(Guid userId,string animeId);
        Task<List<AnimeWatchList>> ViewList(Guid userId);
    }
}
