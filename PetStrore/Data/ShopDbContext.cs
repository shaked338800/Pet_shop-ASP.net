using Microsoft.EntityFrameworkCore;
using PetStrore.Models;

namespace PetStrore.Data
{
    public class ShopDbContext : DbContext
    {
        public virtual DbSet<Animal>? Animals { get; set; }
        public virtual DbSet<Comment>? Comments { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<Admin>? Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasMany(c => c.Comments).WithOne(an => an.Animal).OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=PetStore;Integrated Security=True;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
