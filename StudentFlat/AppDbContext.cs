using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Models;

namespace StudentFlat
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Flat> Flat { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StudentRequest> StudentRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StudentRequest>().HasKey(c => new {c.FlatId, c.StudentId});
            builder.Entity<StudentRequest>().HasOne(c => c.Student).WithMany(k => k.StudentRequests)
                .HasForeignKey(k => k.StudentId);
            builder.Entity<StudentRequest>().HasOne(c => c.Flat).WithMany(k => k.StudentRequests)
                .HasForeignKey(k => k.FlatId);
        }
    }
}
