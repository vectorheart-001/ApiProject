
using ApiProject.Infrastructure.Repository.AnimeRepository;
using ApiProject.Infrastructure.Repository.AnimeWishListRepository;
using ApiProject.Infrastructure.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeWatchListController : ControllerBase
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IAnimeWatchListRepository _animeWatchListRepository;
        private readonly IUserRepository _userRepository;
        public AnimeWatchListController(IAnimeRepository animeRepository,IAnimeWatchListRepository animeWatchListRepository, IUserRepository userRepository)
        {
            _animeRepository = animeRepository;
            _animeWatchListRepository = animeWatchListRepository;
            _userRepository = userRepository;
        }

        [HttpPost("add-to-list")]
        [Authorize]
        public async Task<IActionResult> AddToList(string animeId)
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Guid.TryParse(rawId, out Guid userId);
            var exist = await _animeRepository.Exists(animeId);
            if (exist == false)
            {
                return NotFound();
            }
            
            await _animeWatchListRepository.AddToList(userId, animeId);
            return Ok();
        }
        [HttpDelete("remove-from-list")]
        [Authorize]
        public async Task<IActionResult> Remove(string animeId)
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Guid.TryParse(rawId, out Guid userId);
            var exist = await _animeRepository.Exists(animeId);
            if (exist == false)
            {
                return NotFound();
            }
            await _animeWatchListRepository.RemoveFromList(userId,animeId);
            return Ok();
        }
        [HttpGet("get-list")]
        
        public async Task<IActionResult> ViewList(Guid userId)
        {
             return Ok(await _animeWatchListRepository.ViewList(userId));
        }
        [Authorize]
        [HttpPut("is-watched")]
        public async Task<IActionResult> is_Watched(string animeId)
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Guid.TryParse(rawId, out Guid userId);
            await _animeWatchListRepository.MarkAsWatched(animeId,userId);
            return Ok();
        }


    }
}
