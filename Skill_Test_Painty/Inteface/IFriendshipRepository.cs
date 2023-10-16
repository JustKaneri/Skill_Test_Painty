using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Inteface
{
    public interface IFriendshipRepository
    {
        public Task<string> AddFriendAsync(int account,int friend);

        public Task<List<User>> GetFriendsAsync(int accountId);

        public Task<List<User>> GetMyFriendsAsync(int accountId);
    }
}
