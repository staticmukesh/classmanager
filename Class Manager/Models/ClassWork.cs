using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Class_Manager.Models
{
    public class ClassWork
    {
        [Key]
        public int ClassWorkId { get; set; }

        [Required]
        public String ClassWorkName { get; set; }

        
        public String MaxMark { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}