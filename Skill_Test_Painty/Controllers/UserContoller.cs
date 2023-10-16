using Microsoft.AspNetCore.Mvc;
using Skill_Test_Painty.Inteface;

namespace Skill_Test_Painty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserContoller : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserContoller(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if (users.Count == 0)
                return NotFound("Пользователи не найдены");

            return Ok(users);
        }
    }
}
