using kurs_project.Data;
using kurs_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kurs_project.Controllers
{
    public class TeacherCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeacherCourses
        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers
                .Include(t => t.TeacherCourses)
                    .ThenInclude(tc => tc.Course)
                .ToListAsync();

            return View(teachers);
        }

        // GET: TeacherCourses/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View();
        }

        // POST: TeacherCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int teacherId, int courseId)
        {
            var exists = await _context.TeacherCourses
                .AnyAsync(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);

            if (!exists)
            {
                _context.TeacherCourses.Add(new TeacherCourse
                {
                    TeacherId = teacherId,
                    CourseId = courseId
                });

                await _context.SaveChangesAsync();
                TempData["Success"] = "Course successfully assigned to teacher.";
            }
            else
            {
                TempData["Error"] = "This course is already assigned to the teacher.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: TeacherCourses/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int teacherId, int courseId)
        {
            var tc = await _context.TeacherCourses
                .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);

            if (tc != null)
            {
                _context.TeacherCourses.Remove(tc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Course successfully removed from teacher.";
            }
            else
            {
                TempData["Error"] = "Teacher-course relationship not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
