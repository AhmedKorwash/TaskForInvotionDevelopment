using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student.Mangmnet.Models
{
    public class StudentModels
    {
        public string StudentId { get; set; }
        [Required(ErrorMessage = "Student Name must be not null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Subject Name must be not null")]
        public int Age { get; set; }
        public List<SubjectCheckModel> Subjects { get; set; }
    }
}