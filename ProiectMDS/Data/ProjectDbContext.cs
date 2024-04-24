﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProiectMDS.Models;
namespace ProiectMDS.Data
{
    public class ProjectDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ProjectDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
        }
        public DbSet<User> User { get; set; }

        public DbSet<Card> Card { get; set; }

        public DbSet<Postare> Postare { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<Chirie> Chirie { get; set; }
        public object CardDTO { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chirie>()
                .HasKey(c => new { c.ChirieId, c.PostareId, c.UserId });

            modelBuilder.Entity<Chirie>()
                .HasOne(c => c.User)
                .WithMany(u => u.chirie)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chirie>()
                .HasOne(c => c.Postare)
                .WithMany(p => p.chirie)
                .HasForeignKey(c => c.PostareId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasKey(r => new { r.ReviewId, r.PostareId, r.UserId });

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.review)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Postare)
                .WithMany(p => p.review)
                .HasForeignKey(r => r.PostareId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
