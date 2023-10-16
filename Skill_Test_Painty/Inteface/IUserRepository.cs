using Skill_Test_Painty.Model;

namespace Skill_Test_Painty.Inteface
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();

    }
}
