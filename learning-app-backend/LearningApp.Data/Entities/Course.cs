namespace LearningApp.Data.Entities;

public class Course
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public string Platform { get; set; }
	public string Category { get; set; }
	public float Rating { get; set; }
	public string Url { get; set; }
	public DateTime Created { get; set; } = DateTime.Now;
}
