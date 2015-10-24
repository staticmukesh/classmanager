using Class_Manager.DAL;
using Class_Manager.Models;
using Class_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Class_Manager.Controllers
{
    public class StudentController : Controller
    {
        private ClassContext db = new ClassContext();

        // GET: Student
        public ActionResult Index()
        {
            List<Student> students = db.Students.ToList();
            List<StudentViewModel> model = new List<StudentViewModel>();
            for (int i = 0; i < students.Count; i++)
            {
                Student student = students.ElementAt(i);
                List<AssignedCourse> assignedCourses = new List<AssignedCourse>();

                foreach (var item in student.Courses)
                {
                    assignedCourses.Add(new AssignedCourse
                    {
                        Assigned = true,
                        CourseId = item.CourseId,
                        CourseName = item.CourseName
                    });
                }
                model.Add(new StudentViewModel
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Grade = student.Grade,
                    Address = student.Address,
                    ParentName = student.ParentName,
                    Phone = student.Phone,
                    EMail = student.EMail,
                    Courses = assignedCourses
                });
            }
            return View(model);
        }

        public ActionResult Create()
        {
            var studentViewModel = new StudentViewModel { Courses = PopulateCourseData() };
            return View(studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var student = new Student
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EMail = model.EMail,
                        Phone = model.Phone,
                        ParentName = model.ParentName,
                        Address = model.Address,
                        Grade = model.Grade
                    };

                    AddOrUpdateCourses(student, model.Courses);
                    db.Students.Add(student);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            ICollection<Course> courses = student.Courses;
            List<AssignedCourse> assignedCourses = new List<AssignedCourse>();
            foreach (var item in courses)
            {
                var assignedCourse = new AssignedCourse
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    Assigned = true
                };
                assignedCourses.Add(assignedCourse);
            }

            StudentViewModel model = new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ParentName = student.ParentName,
                Phone = student.Phone,
                Address = student.Address,
                Grade = student.Grade,
                EMail = student.EMail,
                Courses = assignedCourses
            };

            return View(model);
        }

        public ICollection<AssignedCourse> PopulateCourseData()
        {
            var courses = db.Courses;
            var assignedCourses = new List<AssignedCourse>();

            foreach (var item in courses)
            {
                assignedCourses.Add(new AssignedCourse
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    Assigned = false
                });
            }

            return assignedCourses;
        }

        private void AddOrUpdateCourses(Student student, IEnumerable<AssignedCourse> assignedCourses)
        {
            foreach (var assignedCourse in assignedCourses)
            {
                if (assignedCourse.Assigned)
                {
                    var course = new Course { CourseId = assignedCourse.CourseId, CourseName = assignedCourse.CourseName };
                    db.Courses.Attach(course);
                    student.Courses.Add(course);
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            ICollection<Course> courses = student.Courses;
            List<AssignedCourse> assignedCourses = new List<AssignedCourse>();
            foreach (var item in courses)
            {
                var assignedCourse = new AssignedCourse
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    Assigned = true
                };
                assignedCourses.Add(assignedCourse);
            }

            StudentViewModel model = new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ParentName = student.ParentName,
                Phone = student.Phone,
                Address = student.Address,
                Grade = student.Grade,
                EMail = student.EMail,
                Courses = assignedCourses
            };

            return View(model);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}