using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Cumulative1.Models;

namespace C_Cumulative1.Controllers
{
    // This method ia a bind between StudentData Controller and the Views related to Student data
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        // GET: Student/List
        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
           IEnumerable<Student> students= controller.ListStudents();
            return View(students);

        }
        // GET: Student/List/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student student = controller.FindStudent(id);
            return View(student);

        }
    }
}