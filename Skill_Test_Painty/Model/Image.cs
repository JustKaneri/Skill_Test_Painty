using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skill_Test_Painty.Model
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        public int UserId { get; set;}

        public User User { get; set; }
    }
}