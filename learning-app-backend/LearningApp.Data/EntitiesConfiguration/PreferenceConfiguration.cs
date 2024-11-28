using LearningApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Data.EntitiesConfiguration;

public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
{
	public void Configure(EntityTypeBuilder<Preference> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			   .IsRequired()
			   .HasMaxLength(100);

		builder.HasData(
			new Preference { Id = 1, Name = "Programming" },
			new Preference { Id = 2, Name = ".NET" },
			new Preference { Id = 3, Name = "Vue.JS" }
		);
	}
}
