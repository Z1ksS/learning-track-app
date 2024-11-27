using LearningApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Data.EntitiesConfiguration;
public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
	public void Configure(EntityTypeBuilder<Recommendation> builder)
	{
		builder.HasKey(r => r.Id);

		// Визначення зовнішніх ключів(для одного User може бути багато рекомендацій, додатково визначаємо, що якщо видаляється User - видаляється й запис)
		builder.HasOne(r => r.User)
			.WithMany()
			.HasForeignKey(r => r.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		// Один курс на багато рекомендацій
		builder.HasOne(r => r.Course)
			.WithMany()
			.HasForeignKey(r => r.CourseId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
