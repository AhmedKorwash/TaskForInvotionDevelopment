using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student.Mangmnet.Models
{
    public class SubjectModels
    {
        public string SubjectId { get; set; }
        [Required(ErrorMessage = "Subject Name must be not null")]
        public string Name { get; set; }
    }
}