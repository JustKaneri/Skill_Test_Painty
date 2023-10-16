using Microsoft.EntityFrameworkCore;
using Skill_Test_Painty.Data;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Repository
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly DataContext _context;

        public FriendshipRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> AddFriendAsync(int account, int friend)
        {
            if (account == friend)
                throw new Exception("Одинаковые Id");

            var acc = await _context.Users.FindAsync(account);
            var fr = await _context.Users.FindAsync(friend);

            if (acc == null)
                throw new Exception($"Пользователь с Id {account} не найден");

            if (fr == null)
                 throw new Exception($"Пользователь с Id {friend} не найден");

            Friendship friendship = new Friendship();
            friendship.UserId = account;
            friendship.FriendId = friend;

            try
            {
                _context.Friendships.Add(friendship);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return "Пользователь добавлен в друзья";
        }

        public async Task<List<User>> GetMyFriendsAsync(int accountId)
        {
            var acc = await _context.Users.FindAsync(accountId);

            if (acc == null)
                throw new Exception($"Пользователь с Id {accountId} не найден");

            var listUser = await (from us in _context.Users
                            where (from fr in _context.Friendships
                                   where fr.UserId == accountId
                                   select fr.FriendId).Contains(us.Id)
                            select us).ToListAsync();


            return listUser;
        }

        public async Task<List<User>> GetFriendsAsync(int accountId)
        {
            var acc = await _context.Users.FindAsync(accountId);

            if (acc == null)
                throw new Exception($"Пользователь с Id {accountId} не найден");

            var listUser = await (from us in _context.Users
                                  where (from fr in _context.Friendships
                                         where fr.FriendId == accountId
                                         select fr.UserId).Contains(us.Id)
                                  select us).ToListAsync();


            return listUser;
        }
    }
}
