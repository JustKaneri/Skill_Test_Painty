using Skill_Test_Painty.Dto;
using Skill_Test_Painty.Model;
using Profile = AutoMapper.Profile;

namespace Skill_Test_Painty.Helper
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
