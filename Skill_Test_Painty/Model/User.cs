using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Skill_Test_Painty.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set;}
        public string Login { get; set;}
        [JsonIgnore]
        [MinLength(5)]
        public string Password { get; set; }

        private List<Image> _images = new List<Image>();
        [JsonIgnore]
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();


        [JsonIgnore]
        /* Друзья данного пользователя */
        public IEnumerable<User> Friends { get; set; }
        [JsonIgnore]
        /* Те кто добавил пользователя в друзья */
        public IEnumerable<User>  Accounts { get; set; }

        public void AddImage(Image img) 
        { 
            _images.Add(img);
        }
    }
}
