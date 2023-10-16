using Microsoft.EntityFrameworkCore;
using Skill_Test_Painty.Data;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
