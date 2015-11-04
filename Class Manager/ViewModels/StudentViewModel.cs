using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Class_Manager.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Display(Name ="First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        public String Grade { get; set; }

        [Display(Name = "Parent Name")]
        public String ParentName { get; set; }
        public String Phone { get; set; }
        public String EMail { get; set; }
        public String Address { get; set; }
        
        public virtual ICollection<AssignedCourse> Courses { get; set; }
    }
}