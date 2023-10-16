namespace Skill_Test_Painty.Inteface
{
    public interface ISaveImageRepository
    {
        public Task<string> SaveImageAsync(IFormFile file,int Id);

        public Task<List<string>> GetImagesAsync(int id);

        public Task<List<string>> GetFriendImagesAsync(int id,int idFriend);

        public Task<Boolean> AccessCheck(string fileName,int Id);
    }
}
