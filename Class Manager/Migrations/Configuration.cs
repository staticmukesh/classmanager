namespace Class_Manager.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Class_Manager.DAL.ClassContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Class_Manager.DAL.ClassContext context)
        {
            /*
            var courses = new List<Course>
            {
                new Course{CourseId=1050,CourseName="Physics"},
                new Course{CourseId=4022,CourseName="Chemistry"},
                new Course{CourseId=4041,CourseName="Maths"}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            */

            var students = new List<Student>
            {
                new Student {
                    StudentId =100,
                    FirstName ="John",
                    LastName ="Doe",
                    ParentName ="John's Father",
                    Phone ="+1234567890",
                    EMail ="john@doe.com",
                    Address ="John World",
                    Grade ="C"
                }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

        }
    }
}
