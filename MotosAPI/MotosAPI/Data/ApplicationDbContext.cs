using Microsoft.EntityFrameworkCore;
using MotosAPI.Models.UserProfile;

namespace MotosAPI.Data
{
    public class ApplicationDbContext
    {
        public DbSet<UserProfile> UserProfile { get; set; } = default!;
    }
}
