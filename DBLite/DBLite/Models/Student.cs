using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBLite.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
