using LearningApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Data.EntitiesConfiguration;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
	public void Configure(EntityTypeBuilder<Course> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Title)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(c => c.Description)
			.HasMaxLength(1000);

		builder.Property(c => c.Price)
			.HasColumnType("decimal(18,2)");

		builder.Property(c => c.Platform)
			.HasMaxLength(100);

		builder.Property(c => c.Category)
			.HasMaxLength(100);

		builder.Property(c => c.Url)
			.HasMaxLength(500);
	}
}
