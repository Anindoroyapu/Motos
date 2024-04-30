﻿using Microsoft.EntityFrameworkCore;
using MotosAPI.Models.UserProfile;

namespace MotosAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasIndex(u => u.PrimaryEmail)
                .IsUnique();

            // ... other configurations
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //--
        }

        public DbSet<UserProfile> UserProfile { get; set; } = default!;
    }
}
