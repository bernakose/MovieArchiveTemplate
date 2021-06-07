using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MovieArchiveTemplate.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieArchiveTemplate.Models.HelperModels;

namespace MovieArchiveTemplate.Repositories
{
    public partial class MovieArchiveDB : DbContext
    {
        public MovieArchiveDB() : base("name=MovieArchiveDB")
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Movie)
                .WithMany(e => e.Actor)
                .Map(m => m.ToTable("Movie_Actor").MapLeftKey("ActorID").MapRightKey("MovieID"));

            modelBuilder.Entity<Director>()
                .HasMany(e => e.Movie)
                .WithMany(e => e.Director)
                .Map(m => m.ToTable("Movie_Director").MapLeftKey("DirectorID").MapRightKey("MovieID"));

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movie)
                .WithMany(e => e.Genre)
                .Map(m => m.ToTable("Movie_Genre").MapLeftKey("GenreID").MapRightKey("MovieID"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
