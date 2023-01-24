using ApiProject.Domain.DTOs;
using ApiProject.Domain.DTOs.AnimeDTO;
using ApiProject.Domain.Entities;
using ApiProject.Infrastructure.Repository.AnimeRepository;
using ApiProject.Infrastructure.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IUserRepository _userRepository;
        public AnimeController(IAnimeRepository animeRepository, IUserRepository userRepository) 
        {
            _animeRepository= animeRepository;
            _userRepository= userRepository;
        }
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByName(string name,int page =1)
        {
            var list = await _animeRepository.GetByName(name,page);
            if (list.Item2 < page || page < 0)
            {
                return BadRequest();
            }
            return Ok(list);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            var list = await _animeRepository.GetAll(page);
            if (list.Item2 < page || page < 0)
            {
                return BadRequest();
            }
            return Ok(list);
        }
        [HttpPut("edit-anime")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAnime(string id,[FromBody]AnimeEditDTO anime)
        {
            if (await _animeRepository.Exists(id) == false)
            {
                return BadRequest();
            }
            await _animeRepository.Edit(id,anime);
            return Ok();
        }
        [HttpGet("get-by-genre")]
        public async Task<IActionResult> GetByGenre(string genre,int page)
        {
            var list = await _animeRepository.GetByGenre(genre, page);
            if (list.Item2 < page || page < 0)
            {
                return BadRequest();
            }
            return Ok(list);
        }
        [HttpGet("get-stats")]
        public async Task<FileContentResult> GetAnimeStats()
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var bytes = await _animeRepository.PdfFileStats();
            return File(bytes, "application/pdf", "stats.pdf");
        }
        
    }
}
