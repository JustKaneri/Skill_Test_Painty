using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skill_Test_Painty.Dto;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : Controller 
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository,IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Токен</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(string login,string password)
        {
            string token = "";

            try
            {
                token = await _authRepository.LogInAsync(login, password);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(token);
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="userDto">Данные для регистрации</param>
        /// <returns>Токен</returns>
        [HttpPost("regestry")]
        public async Task<IActionResult> Regestry(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            string token = "";

            try
            {
                token = await _authRepository.RegestryAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(token);
        }
    }
}
