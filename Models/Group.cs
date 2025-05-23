namespace kurs_project.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
        }
    }
}
