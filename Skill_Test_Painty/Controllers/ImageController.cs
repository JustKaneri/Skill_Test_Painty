using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;
using System.Security.Claims;

namespace Skill_Test_Painty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ImageController : Controller
    {
        private readonly ISaveImageRepository _imageRepository;
        private readonly IConfiguration _configuration;

        public ImageController(ISaveImageRepository imageRepository,IConfiguration configuration)
        {
            _imageRepository = imageRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Загрузга изображения
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("image")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }


            try
            {
                var result = await _imageRepository.SaveImageAsync(file, idUser);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok("Файл загружен");
        }

        /// <summary>
        /// Список изображений пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("my/image")]
        [Authorize]
        public async Task<IActionResult> MyImage()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }

            var lst = await _imageRepository.GetImagesAsync(idUser);

            return Ok(lst);
        }

        /// <summary>
        /// Получить само изображение
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("image")]
        [Authorize]
        public async Task<IActionResult> GetImage(string fileName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }


            try
            {
                var result = await _imageRepository.AccessCheck(fileName, idUser);

                if (!result)
                    return BadRequest("Нет прав к данному изображению");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

            string patch = Path.Combine(_configuration.GetSection("Image:Path").Value, fileName);

            if(System.IO.File.Exists(patch))
            {
                byte[] bt = System.IO.File.ReadAllBytes(patch);
                return File(bt, "image/png");
            }
            else
            {
                return BadRequest("Файл не найден");
            }
        }

        /// <summary>
        /// Изображение пользователя в друзьях
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("my/friend/{Id}/images")]
        [Authorize]
        public async Task<IActionResult> FriendImage(int Id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUser = -1;
            if (identity != null)
            {
                idUser = int.Parse(identity.FindFirst("id").Value);
            }

            var lst = await _imageRepository.GetFriendImagesAsync(idUser,Id);

            return Ok(lst);
        }
    }
}
