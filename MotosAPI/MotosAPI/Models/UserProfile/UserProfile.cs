using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MotosAPI.Models.UserProfile
{
    
        [Table("UserProfile", Schema = "public")]
        public class UserProfile
        {
        internal string? Token;

        [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [MaxLength(300)]
            public string FirstName { get; set; } = "";

            [MaxLength(300)]
            public string LastName { get; set; } = "";

            [MaxLength(300)]
            public string Organization { get; set; } = "";

            [MaxLength(100)]
            public string UserName { get; set; } = "";

            [MaxLength(512)]
            public string Password { get; set; } = "";

            [MaxLength(100)]
            public string PrimaryEmail { get; set; } = "";

            [MaxLength(100)]
            public string PrimaryPhone { get; set; } = "";

            [MaxLength(300)]
            public string PrimaryAddress { get; set; } = "";

            [MaxLength(20)]
            public string Gender { get; set; } = "";

            public long DateOfBirth { get; set; }

            public long TimeCreated { get; set; }

        }
    
}
