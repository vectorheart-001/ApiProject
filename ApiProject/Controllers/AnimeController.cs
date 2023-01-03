using ApiProject.Domain.DTOs;
using ApiProject.Domain.DTOs.AnimeDTO;
using ApiProject.Domain.Entities;
using ApiProject.Infrastructure.Repository.AnimeRepository;
using ApiProject.Infrastructure.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(await _animeRepository.GetByName(name,page =1));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            return Ok(await _animeRepository.GetAll(page));
        }
        [HttpPut("edit-anime")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAnime(string id,AnimeEditDTO anime)
        {
            if (await _animeRepository.Exist(id) == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpGet("get-by-genre")]
        public async Task<IActionResult> GetByGenre(string genre,int page = 1)
        {
            return Ok(await _animeRepository.GetByName(genre,page));
        }
    }
}
