using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Class_Manager.DAL;
using Class_Manager.Models;
using System.Data.Entity.Infrastructure;
using Class_Manager.ViewModels;

namespace Class_Manager.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private ClassContext db = new ClassContext();

        // GET: Course
        public ActionResult Index(int? SelectedDepartment)
        {
            IQueryable<Course> courses = db.Courses.OrderBy(d => d.CourseId);
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.classwork = db.ClassWorks.Where(m => m.CourseId == course.CourseId).ToList();
            return View(course);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName")]Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseToUpdate = db.Courses.Find(id);

            if (TryUpdateModel(courseToUpdate, "",
               new string[] { "CourseId", "CourseName" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(courseToUpdate);
        }



        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<ClassWork> classworks = db.ClassWorks.Where(m => m.CourseId == id).ToList();
            foreach (ClassWork work in classworks)
            {
                db.ClassWorks.Remove(work);
            }

            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Classwork(int id)
        {
            ClassWork model = new ClassWork { CourseId = id };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Classwork(ClassWork model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ClassWorks.Add(model);

                    Course course = db.Courses.Find(model.CourseId);
                    List<Student> students = course.Students.ToList();
                    foreach (Student student in students)
                    {
                        db.Grades.Add(new Grade
                        {
                            ClassWorkId = model.ClassWorkId,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Marks = "",
                            Comment = "",
                            CourseName = course.CourseName,
                            ClassWorkName = model.ClassWorkName
                        });
                    }

                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = model.CourseId });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(model);
        }

        public ActionResult Grades(int id)
        {
            ClassWork classwork = db.ClassWorks.Find(id);
            if (classwork == null)
            {
                return HttpNotFound();
            }
            Course course = db.Courses.Find(classwork.CourseId);
            List<Student> students = course.Students.ToList();
            List<Grade> grades = db.Grades.Where(m => m.ClassWorkId == id).ToList();

            GradeViewModel model = new GradeViewModel();

            model.CourseId = course.CourseId;
            model.CourseName = course.CourseName;
            model.ClassWorkName = classwork.ClassWorkName;
            model.ClassWorkId = classwork.ClassWorkId;
            model.MaxMarks = classwork.MaxMark;
            model.Grades = new List<Grade>();

            foreach (Student student in students)
            {
                bool found = false;
                foreach (Grade grade in grades)
                {
                    if (grade.StudentId == student.StudentId)
                    {
                        model.Grades.Add(grade);
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    continue;
                }

                Grade newGrade = new Grade
                {
                    ClassWorkId = classwork.ClassWorkId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Marks = "",
                    Comment = "",
                    StudentId = student.StudentId,
                    CourseName = course.CourseName,
                    ClassWorkName = classwork.ClassWorkName
                };
                db.Grades.Add(newGrade);
                model.Grades.Add(newGrade);
            }

            db.SaveChanges();

            return View(model);
        }

        [HttpPost]
        public ActionResult Grades(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (Grade grade in model.Grades)
                {
                    Grade oldGrade = db.Grades.Find(grade.GradeId);
                    db.Entry(oldGrade).CurrentValues.SetValues(grade);
                }
                db.SaveChanges();

                return RedirectToAction("Details", new { id = model.CourseId });
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
