using Microsoft.EntityFrameworkCore;
using Skill_Test_Painty.Data;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Repository
{
    public class ImageRepository : ISaveImageRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IFriendshipRepository _friendshipRepository;

        public ImageRepository(DataContext context, IConfiguration configuration, IFriendshipRepository friendshipRepository)
        {
            _context = context;
            _configuration = configuration;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<bool> AccessCheck(string fileName, int id)
        {
            var image = await _context.Images.Where(i => i.FileName == fileName).FirstOrDefaultAsync();

            if (image == null)
                throw new Exception("Изображение не найдено");

            if (image.UserId == id)
                return true;

            int idFriend = image.UserId;

            var fr = await _friendshipRepository.GetFriendsAsync(id);

            if (fr.Where(f => f.Id == idFriend).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        public async Task<List<string>> GetFriendImagesAsync(int id, int idFriend)
        {
            var fr = await _friendshipRepository.GetFriendsAsync(id);

            if (fr.Where(f => f.Id == idFriend).FirstOrDefault() == null)
            {
                throw new Exception("Пользователь не добавил вас в друзья");
            }

            return await (from img in _context.Images where img.UserId == idFriend select img.FileName).ToListAsync();
        }

        public async Task<List<string>> GetImagesAsync(int id)
        {
            return await (from img in _context.Images where img.UserId == id select img.FileName).ToListAsync();
        }

        public async Task<string> SaveImageAsync(IFormFile file, int Id)
        {
            Image image = new Image();
            image.UserId = Id;
            string fileName = "";
            string patch = "";

            fileName = Guid.NewGuid() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
            patch = Path.Combine(_configuration.GetSection("Image:Path").Value, fileName);
            FileStream fs = null;


            try
            {
                fs = new FileStream(patch, FileMode.Create);
                await file.CopyToAsync(fs);

                image.FileName = fileName;
            }
            catch (Exception)
            {
                throw new Exception("Не удалось сохранить файл");
            }
            finally
            {
                fs.Close();
            }

            await AddImageInDb(image);

            return fileName;
        }

        private async Task AddImageInDb(Image img)
        {
            try
            {
                User us = await _context.Users.Where(u => u.Id == img.UserId).FirstOrDefaultAsync();
                us.AddImage(img);

                try
                {
                    _context.Users.Update(us);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
