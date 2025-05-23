using System.ComponentModel.DataAnnotations;

namespace kurs_project.Models
{
    public class TeacherCourse
    {
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;


        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}