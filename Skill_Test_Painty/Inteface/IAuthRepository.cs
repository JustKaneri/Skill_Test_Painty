using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Inteface
{
    public interface IAuthRepository
    {
        public Task<string> LogInAsync(string login,string password);
        public Task<string> RegestryAsync(User user);
    }
}
