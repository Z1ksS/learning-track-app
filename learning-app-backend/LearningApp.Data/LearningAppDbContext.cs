using LearningApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Data;

public class LearningAppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Recommendation> Recommendations { get; set; }

	public LearningAppDbContext(DbContextOptions<LearningAppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(LearningAppDbContext).Assembly);
	}
}
