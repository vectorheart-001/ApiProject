using ApiProject.Domain.DTOs;
using ApiProject.Domain.DTOs.AnimeDTO;
using ApiProject.Domain.Entities;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Repository.AnimeRepository
{
    public interface IAnimeRepository
    {
        Task<Tuple<List<Anime>, int>> GetByGenre(string genre,int page = 1);
        Task<Tuple<List<Anime>, int>> GetByName(string name,int page = 1);
        
        Task<Anime> GetById(Guid id);
        Task<Tuple<List<Anime>,int>> GetAll(int page = 1);
        Task Edit(string id,AnimeEditDTO anime);
        Task<bool> Exists(string id);
        Task<byte[]> TextFileStats();
    }
}
