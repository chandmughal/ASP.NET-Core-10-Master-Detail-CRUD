using Microsoft.EntityFrameworkCore;
using StudentMarksMasterDetail.Models;
namespace StudentMarksMasterDetail.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Mark> Marks => Set<Mark>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasMany(s => s.Marks).WithOne(m => m.Student)
            .HasForeignKey(m => m.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}