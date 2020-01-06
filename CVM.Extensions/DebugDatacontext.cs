using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVM.Extensions
{
    public class DebugDatacontext : DbContext
    {

        public DbSet<Project> Projects { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ProjectTranslation> ProjectTranslations { get; set; }

        public DebugDatacontext() { }
        public DebugDatacontext(DbContextOptions<DebugDatacontext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DBGWPFDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectTranslations)
                .WithOne(pt => pt.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTranslation>()
                .HasOne(pt => pt.Language);

        }
    }

    public class Company
    {
        /// <summary>
        /// Name of the Company
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Date joined 
        /// </summary>
        [Required]
        public DateTime Joined { get; set; }

        /// <summary>
        /// Date left
        /// </summary>
        public Nullable<DateTime> Left { get; set; }
        public int Id { get; set; }
    }


    public class Project
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Date left
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }

        public ICollection<ProjectTranslation> ProjectTranslations { get; set; } = new List<ProjectTranslation>();

    }
    public class ProjectTranslation
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public Project Project { get; set; }

    }

    public class Language
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }
}
