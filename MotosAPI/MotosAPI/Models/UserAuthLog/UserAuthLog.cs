using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotosAPI.Models.UserAuthLog
{
    [Table("UserAuthLog", Schema = "public")]
    public class UserAuthLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(300)]
        public string? LoginKey { get; set; }

        [Required]
        public int Creator { get; set; }

        [Required]
        public long TimeCreated { get; set; }

        [Required]
        public long TimeUpdated { get; set; }

        [Required]
        public long TimeDeleted { get; set; }

        [Required]
        [StringLength(1000)]
        public string? LastLoginInfo { get; set; }

    }
}
