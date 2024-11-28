using LearningApp.Data.Entities.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Data.EntitiesConfiguration;

public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
{
	public void Configure(EntityTypeBuilder<UserPreference> builder)
	{
		builder.HasKey(up => new { up.UserId, up.PreferenceId }); 

		builder.HasOne(up => up.User)
			   .WithMany(u => u.UserPreferences)
			   .HasForeignKey(up => up.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(up => up.Preference)
			   .WithMany(p => p.UserPreferences)
			   .HasForeignKey(up => up.PreferenceId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
