using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotosAPI.Models.UserProfile
{

    [Table("UserProfile", Schema = "public")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(300)]
        public string FirstName { get; set; } = "";

        [MaxLength(300)]
        public string LastName { get; set; } = "";

        [MaxLength(100)]
        public string UserName { get; set; } = "";

        [MaxLength(512)]
        public string Password { get; set; } = "";

        [MaxLength(100)]
        public string PrimaryEmail { get; set; } = "";

        [MaxLength(100)]
        public string PrimaryPhone { get; set; } = "";

        [MaxLength(20)]
        public string Gender { get; set; } = "";

        public long DateOfBirth { get; set; }

        [MaxLength(1000)]
       public string Token { get; set; } = "";

       public long TokenTime { get; set; }

        
    }

}
