﻿using LearningApp.Data.Entities.UserData;

namespace LearningApp.Data.Entities;

public class Preference
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<UserPreference> UserPreferences { get; set; }
}
