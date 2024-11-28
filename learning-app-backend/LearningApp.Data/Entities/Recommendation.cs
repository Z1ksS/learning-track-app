using LearningApp.Data.Entities.UserData;

namespace LearningApp.Data.Entities;

public class Recommendation
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
	public int CourseId { get; set; }
	public Course Course { get; set; }
	public DateTime RecommendedAt { get; set; } = DateTime.UtcNow;
}
