﻿using LearningApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.HasIndex(u => u.Email).IsUnique();

		builder.Property(u => u.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(u => u.Role)
		   .HasDefaultValue("User");
	}
}
