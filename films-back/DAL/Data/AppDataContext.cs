using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FilmCategory> FilmCategory { get; set; }
        public DbSet<UserDetail> Users { get; set; }
        public DbSet<Imdb> Imdb { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(c => new { c.FilmId, c.CategoryId });
            });
            var film = modelBuilder.Entity<Film>();
            film
                .HasMany(f => f.Categories)
                .WithMany(c => c.Films)
                .UsingEntity<FilmCategory>(
                    x => x.HasOne(f => f.Category)
                    .WithMany(x => x.FilmCategories).HasForeignKey(c => c.CategoryId),
                    x => x.HasOne(f => f.Film)
                    .WithMany(x => x.FilmCategories).HasForeignKey(c => c.FilmId)
                    );
            film
                .HasMany(f => f.Imdbs)
                .WithOne(d => d.Film)
                .HasForeignKey(f => f.FilmId);

            var Images = modelBuilder.Entity<Image>();
            Images
                .HasOne(f => f.Films)
                .WithMany(i => i.Images)
                .HasForeignKey(f => f.FilmId);
            //.OnDelete(DeleteBehavior.Restrict);

            var comments = modelBuilder.Entity<Comment>();
            comments
                .HasOne(c=>c.Film)
                .WithMany(f=>f.Comments)
                .HasForeignKey(c=>c.FilmId);
                //.OnDelete(DeleteBehavior.Restrict);

            comments
                .HasOne(c=>c.User)
                .WithMany(u=>u.Comments)
                .HasForeignKey(c=>c.UserId);

            var imdb= modelBuilder.Entity<Imdb>();
            imdb
                .HasOne(i => i.Film)
                .WithMany(f => f.Imdbs)
                .HasForeignKey(i => i.FilmId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
