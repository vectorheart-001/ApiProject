using ApiProject.Api.Authentication.TokenGenerators;
using ApiProject.Api.Authentication.TokenValidators;
using ApiProject.Domain.Entities;
using ApiProject.Infrastructure.Repository.RefreshTokenRepository;
using ApiProject.Infrastructure.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly Authenticator _authenticator;
        private readonly ValidateRefreshToken _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public UserController(IUserRepository userRepository, ValidateRefreshToken refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository, Authenticator authenticator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _authenticator = authenticator;

            _userRepository = userRepository;

            _refreshTokenValidator = refreshTokenValidator;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest();
            }
            var emailValidate = await _userRepository.GetByEmail(request.Email);
            if (emailValidate != null)
            {
                return Conflict();
            }
            var nameValidate = await _userRepository.GetByUserName(request.Name);
            if (nameValidate != null)
            {
                return Conflict();
            }


            User user = new User()
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
            };
            await _userRepository.Create(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User user = await _userRepository.GetByUserName(login.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (user.Password != login.Password)
            {
                return Unauthorized();
            }
            AuthenticatedResponse authenticatedUser = await _authenticator.Authenticate(user);
            return Ok(authenticatedUser);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool isValidRefreshToken = _refreshTokenValidator.is_Valid(token);
            if (!isValidRefreshToken)
            {
                return BadRequest("Expired Token");
            }
            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(token);
            if (refreshTokenDTO == null)
            {
                return NotFound("Not found :(");
            }
            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);
            User user = await _userRepository.GetById(refreshTokenDTO.UserId);
            AuthenticatedUserResponse authenticatedUser = await _authenticator.Authenticate(user);
            return Ok(authenticatedUser);
        }
        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> LogOut()
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Guid.TryParse(rawId, out Guid userId);

            if (userId.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return BadRequest("Invalid token");
            }
            await _refreshTokenRepository.DeleteAll(userId);
            return Ok("LoggedOut");
        }


    }
}