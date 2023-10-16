using Microsoft.EntityFrameworkCore;
using Skill_Test_Painty.Model;
using System.Diagnostics.Contracts;

namespace Skill_Test_Painty.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }   


        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(us =>
            {
                us.HasMany(f => f.Friends)
                  .WithMany(a => a.Accounts)
                  .UsingEntity<Friendship>(
                    u => u.HasOne<User>().WithMany().HasForeignKey(fk => fk.UserId),
                    f => f.HasOne<User>().WithMany().HasForeignKey(fk => fk.FriendId)
                    );
            });
        }
    }
}
