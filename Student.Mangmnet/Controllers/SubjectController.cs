using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Student.Mangmnet.Models;
using Student.DAL;
using System.Threading.Tasks;
namespace Student.Mangmnet.Controllers
{
    public class SubjectController : Controller
    {
        public ActionResult Index(string id = null)
        {
            SubjectModels model = new SubjectModels
            {
                SubjectId = Guid.NewGuid().ToString()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(SubjectModels model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            try
            {
                StudnetDB studnetDB = new StudnetDB();
                studnetDB.Subjects.Add(entity: new Subject() { SubjectId = model.SubjectId, SubjectName = model.Name });
                await studnetDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch { return View("Error"); }
            
        }
    }
}