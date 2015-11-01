using Class_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Manager.ViewModels
{
    public class GradeViewModel
    {
        public int ClassWorkId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ClassWorkName { get; set; }
        public string MaxMarks { get; set; }
        
        public List<Grade> Grades { get; set; } 
    }
}