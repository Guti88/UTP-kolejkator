using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Student student)
        {
            Models.Queue queue = new Models.Queue();
            student.queue1 = queue.getStudent(student.index, 1);
            student.queue2 = queue.getStudent(student.index, 2);
            student.queue3 = queue.getStudent(student.index, 3);

            return View(student);
        }
    }
}