using Student.DAL;
using Student.Mangmnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Mangmnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                var studList = new List<StudentModels>();
                foreach (var stu in studnetDB.Students.ToList())
                {
                    //here we can collect subjects for each student
                    var subjects = new List<SubjectCheckModel>();
                    foreach (var subj in stu.Subjects)
                    {
                        subjects.Add(new SubjectCheckModel()
                        {
                            Id = subj.SubjectId,
                            Checked = true,
                            Name = subj.SubjectName
                        });
                    }
                    //assigne student information to student list
                    studList.Add(new StudentModels()
                    {
                        StudentId = stu.Id,
                        Name = stu.Name,
                        Age = Convert.ToInt32(stu.Age),
                        Subjects = subjects
                    });
                }
                ViewData["StudentList"] = studList;
                return View();
            }
            catch { return View("Error"); } 
        }
        
    }
}