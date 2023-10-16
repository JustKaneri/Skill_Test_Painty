using Microsoft.EntityFrameworkCore;
using Skill_Test_Painty.Data;
using Skill_Test_Painty.Inteface;
using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Repository
{
    public class AuthJwtRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly JwtTokenRepository _tokenRepository;

        public AuthJwtRepository(DataContext context,JwtTokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LogInAsync(string login, string password)
        {
            User user = await _context.Users.Where(us => us.Login == login && us.Password == password).FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            return _tokenRepository.GenerateJwtToken(user);
        }

        public async Task<string> RegestryAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _tokenRepository.GenerateJwtToken(user);
        }
    }
}
