using api.src.entities;
using Microsoft.EntityFrameworkCore;

namespace api.src.dataContext
{
  public class DataContext : DbContext
  {
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserEntity>()
        .HasMany(user => user.Tasks)
        .WithOne(task => task.User)
        .HasForeignKey(task => task.UserId);
    }
  }
}