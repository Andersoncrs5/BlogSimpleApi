using BlogSimpleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleApi.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }           
        public DbSet<Like> Likes { get; set; }           
        public DbSet<Post> Posts { get; set; }           
        public DbSet<Comment> Comments { get; set; }     
        public DbSet<Category> Categories { get; set; }  
        public DbSet<FavoritePost> FavoritePosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }


    }
}
