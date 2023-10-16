using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skill_Test_Painty.Model
{
    [Table("Friendship")]
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        /* Пользователь */
        public int UserId { get; set; }
        /* Тот кого он добавил в друзья */
        public int FriendId { get; set; }

        //public User User { get; set; }

        //public User  Friend { get; set; }
    }
}
