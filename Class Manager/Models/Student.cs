using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Class_Manager.Models
{
    public class Student
    {
        public Student()
        {
            Courses = new List<Course>();
        }

        [Key]
        public int StudentId { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Grade { get; set; }

        [Required]
        public String ParentName { get; set; }

        [Required]
        public String Phone { get; set; }

        [Required]
        [EmailAddress]
        public String EMail { get; set; }

        [Required]
        public String Address { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}