using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Student.Mangmnet.Models;
using Student.DAL;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Student.Mangmnet.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id = null)
        {
            try
            {
                var list = new List<SubjectCheckModel>();
                StudnetDB studnetDB = new StudnetDB();
                foreach (var sub in studnetDB.Subjects.ToList())
                {
                    list.Add(item: new SubjectCheckModel() { Id = sub.SubjectId, Name = sub.SubjectName, Checked = false });
                }

                StudentModels model = new StudentModels
                {
                    StudentId = Guid.NewGuid().ToString(),
                    Subjects = list
                };
                return View(model);
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(StudentModels model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                var student = new DAL.Student() { Id = model.StudentId, Age = model.Age, Name = model.Name };

                foreach (var sub in model.Subjects.Where(s => s.Checked))
                {
                    student.Subjects.Add(studnetDB.Subjects.Where(s => s.SubjectId == sub.Id).First());
                }
                studnetDB.Students.Add(student);
                await studnetDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch { return View("Error"); }
            
        }
        public ActionResult Edite(string id)
        {
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                var subjects = new List<SubjectCheckModel>();

                var student = studnetDB.Students.Where(s => s.Id == id).First();
                if (student == null)
                    return View("Error");

                StudentModels model = new StudentModels
                {
                    StudentId = id,
                    Age = (int)student.Age,
                    Name = student.Name,
                    Subjects = subjects
                };

                foreach (var subj in studnetDB.Subjects.ToList())
                {
                    subjects.Add(new SubjectCheckModel()
                    {
                        Id = subj.SubjectId,
                        Checked = student.Subjects.Contains(subj),
                        Name = subj.SubjectName
                    });
                }
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edite(StudentModels model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                var student = studnetDB.Students.Where(s => s.Id == model.StudentId).First();
                if (student == null)
                    return View("Error");

                foreach (var sub in model.Subjects)
                {
                    if (sub.Checked)
                        student.Subjects.Add(studnetDB.Subjects.Where(s => s.SubjectId == sub.Id).First());
                    else
                    {
                        var tempSub = student.Subjects.Where(s => s.SubjectId == sub.Id).FirstOrDefault();
                        if (student.Subjects.Contains(tempSub))
                            student.Subjects.Remove(tempSub);
                    }
                }
                student.Age = model.Age;
                await studnetDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch { return View("Error"); }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                var student = studnetDB.Students.Where(s => s.Id == id).First();
                foreach (var sup in student.Subjects)
                {
                    var subject = studnetDB.Subjects.Where(s => s.SubjectId == sup.SubjectId).First();
                    if (subject == null)
                        return View("Error");
                    subject.Students.Remove(student);
                }
                studnetDB.Students.Remove(student);
                await studnetDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch { return View("Error"); }
        }
    }
}