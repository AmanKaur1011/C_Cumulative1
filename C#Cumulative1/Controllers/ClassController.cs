using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Cumulative1.Models;

namespace C_Cumulative1.Controllers
{
    // This method ia a bind between ClassData Controller and the Views related to Class data
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        // GET: Class/List
        public ActionResult List()
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> classes = controller.ListClasses();
            return View(classes);


        }
        // GET: Class/List/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class separateclass = controller.FindClass(id);
            return View(separateclass);


        }
    }
}