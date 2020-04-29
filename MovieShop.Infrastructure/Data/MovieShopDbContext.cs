using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<MovieCrew> MovieCrews { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.DateOfBirth).HasColumnType("datetime2");
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.LockoutEndDate).HasColumnType("datetime2");
            builder.Property(u => u.LastLoginDateTime).HasColumnType("datetime2");
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(2084);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(c => c.Name).HasMaxLength(20);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseDateTime).HasColumnType("datetime2");
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.Property(mc => mc.Department).HasMaxLength(128);
            builder.Property(mc => mc.Job).HasMaxLength(128);
            builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department, mc.Job });
            builder.HasOne(mc => mc.Movie).WithMany(m => m.MovieCrews).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(g => g.MovieCrews).HasForeignKey(mc => mc.CrewId);

        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.Property(mc => mc.Character).HasMaxLength(450);
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
            builder.HasOne(mc => mc.Movie).WithMany(m => m.MovieCasts).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(g => g.MovieCasts).HasForeignKey(mc => mc.CastId);

        }
         
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);   
        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);

        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Title);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Ignore(m=>m.Rating);
        }

        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            //[Table("Genre")]
            //fluent api here
            builder.ToTable("Genre");
            //[Key]
            builder.HasKey(g => g.Id);

            //[Required], [MaxLength(64)]
            builder.Property(g => g.Name).IsRequired().HasMaxLength(64);

        }
    }
}
