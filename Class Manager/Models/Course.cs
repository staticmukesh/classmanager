using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Class_Manager.Models
{
    public class Course
    {
        public Course()
        {
            Students = new List<Student>();
        }

        [Required]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}