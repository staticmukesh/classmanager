using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Manager.ViewModels
{
    public class AssignedCourse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool Assigned { get; set; }
    }
}