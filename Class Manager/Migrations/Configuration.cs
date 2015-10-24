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
                    FirstName ="Mukesh",
                    LastName ="Sharma",
                    ParentName ="Bhim Singh",
                    Phone ="+919711923230",
                    EMail ="staticmukesh@gmail.com",
                    Address ="A-47, Amar Colony, Nangloi, Delhi-110041",
                    Grade ="C"
                },

                new Student {
                    StudentId =101,
                    FirstName ="Rohit",
                    LastName ="Yadav",
                    ParentName ="Random Father",
                    Phone ="+91-9825545563",
                    EMail ="mukesh.6182@gmail.com",
                    Address ="A-47, Amar Colony, Nangloi, Delhi-110041",
                    Grade ="D"
                }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

        }
    }
}
