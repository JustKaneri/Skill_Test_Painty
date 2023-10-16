using System.ComponentModel.DataAnnotations;

namespace Skill_Test_Painty.Dto
{
    public class UserDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
