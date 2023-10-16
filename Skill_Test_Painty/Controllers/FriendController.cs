using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;
using System.Security.Claims;

namespace Skill_Test_Painty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class FriendController : Controller
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendController(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        /// <summary>
        /// Добавить друга
        /// </summary>
        /// <param name="IdFriend"></param>
        /// <returns></returns>
        [HttpPost("friend")]
        [Authorize]
        public async Task<IActionResult> AddFriend(int IdFriend)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }

            string res;
            
            try
            {
                res = await _friendshipRepository.AddFriendAsync(idUser, IdFriend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(res);
        }

        /// <summary>
        /// Получить пользователей добавленных в друзья
        /// </summary>
        /// <returns></returns>
        [HttpPost("my/friends")]
        [Authorize]
        public async Task<IActionResult> GetMyFriend()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }

            List<User> res;

            try
            {
                res = await _friendshipRepository.GetMyFriendsAsync(idUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (res.Count == 0)
                return NotFound("Друзья не найдены");

            return Ok(res);
        }


        /// <summary>
        /// Список у когов в друзьях находится данный пользователь
        /// </summary>
        /// <returns></returns>
        [HttpPost("friends")]
        [Authorize]
        public async Task<IActionResult> GetFriend()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }

            List<User> res;

            try
            {
                res = await _friendshipRepository.GetFriendsAsync(idUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (res.Count == 0)
                return NotFound("Друзья не найдены");

            return Ok(res);
        }
    }
}
