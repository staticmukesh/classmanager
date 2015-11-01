using Class_Manager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Class_Manager.DAL
{
    public class ClassContext : DbContext
    {
        public ClassContext() : base("DefaultConnection")
        {}

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassWork> ClassWorks { get; set; }
        public DbSet<Grade> Grades{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Student>()
                .HasMany(c => c.Courses).WithMany(i => i.Students)
                .Map(t => t.MapLeftKey("CourseId")
                    .MapRightKey("StudentId")
                    .ToTable("CourseStudent"));

        }
    }
}