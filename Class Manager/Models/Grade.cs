using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Manager.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassWorkId { get; set; }
        public String ClassWorkName { get; set; }
        public string CourseName { get; set; }
        public string Marks { get; set; }
        public string Comment { get; set; }
    }
}