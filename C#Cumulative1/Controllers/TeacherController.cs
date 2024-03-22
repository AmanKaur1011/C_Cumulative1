using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Cumulative1.Models;

namespace C_Cumulative1.Controllers
{
    // This method ia a bind between TeacherData Controller and the Views related to Teacher data
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        // GET: Teacher/List
        public ActionResult List(string SearchKey=null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher>teachers=controller.ListTeachers(SearchKey);

            return View(teachers);
        }

        // GET: Teacher/List/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teacher = controller.FindTeacher(id);

            return View(teacher);
        }
    }
}