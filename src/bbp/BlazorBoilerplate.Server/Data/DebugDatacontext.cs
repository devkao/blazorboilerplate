using BlazorBoilerplate.Server.Data.Configurations;
using BlazorBoilerplate.Server.Data.Interfaces;
using BlazorBoilerplate.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BlazorBoilerplate.Shared.Dto.CUSTOMIZED;

namespace BlazorBoilerplate.Server.Data
{
    public class DebugDatacontext : DbContext
    {
        public DbSet<Language> Languages { get; set; }

        //  public DbSet<Project> Projects { get; set; }
        // public DbSet<Company> Companies { get; set; }
        // public DbSet<ProjectTranslation> ProjectTranslations { get; set; }

        public DebugDatacontext() { }
        public DebugDatacontext(DbContextOptions<DebugDatacontext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DBGWPFDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.ProjectTranslations)
            //    .WithOne(pt => pt.Project)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProjectTranslation>()
            //    .HasOne(pt => pt.Language);

        }
    }
}
