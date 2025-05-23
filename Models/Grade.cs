using System.ComponentModel.DataAnnotations;

namespace kurs_project.Models
{
	public class Grade
	{
		public int Id { get; set; }

		public int StudentId { get; set; }
		public Student? Student { get; set; }

		public int CourseId { get; set; }
		public Course? Course { get; set; }

		[Range(1, 10)]
		public int Score { get; set; }

		public string? Comment { get; set; }
	}
}
