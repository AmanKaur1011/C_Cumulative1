using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using C_Cumulative1.Models;

namespace C_Cumulative1.Controllers
{
    // This controler is a bind between TeacherData Controller and the Views related to Teacher data
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
        //GET: Teacher/New -> directs to the webpage where user can add a new teacher to the list
        public ActionResult New()
        {
           //Navigate to the Views/Teacher/New.cshtml
            return View();
        }


        //GET : /Teacher/Ajax_New-> directs to the webpage where user can enter the new teacher to the database using AJAX
        public ActionResult Ajax_New()
        {            //Navigate to the Views/Teacher/Ajax_New.cshtml
            return View();

        }
     
        /// <summary>
        /// This method will direct to a webpage  asking to delete a particular teacher using AJAX.
        /// </summary>
        /// <param name="id"> the id of the  selected teacher that need to be deleted </param>
        /// <returns> a view confirming the deletion of  selected teacher</returns>
        /// <example> GET :/Teacher/Ajax_Delete/{id} ->  render a web page (view) confirming if the user want to  delete the selected teacher</example>
        public ActionResult Ajax_Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);

            return View(selectedTeacher);
            
        }

        /// <summary>
        /// This method will  direct to a view(webpage) confirming the choice of deleting a  selected teacher
        /// </summary>
        /// <param name="id"> the id of the  selected teacher that need to be deleted </param>
        /// <returns> a view confirming the deletion of  selected teacher</returns>
        /// <example> GET :/Teacher/DeleteConfirm/{id} ->  render a web page (view) confirming if the user want to  delete the selected teacher</example>
       
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);

            //Navigate to the Views/Teacher/DeleteConfirm.cshtml
            return View(selectedTeacher);
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //POST : /Teacher/Create-> This method is taking the input form fields for a new teacher and assigning it to a teacher object which is being sent  to the AddTeacher method in the TeacherData controller

        [HttpPost]
        public ActionResult Create (string TeacherFname, string TeacherLname, string EmployeeNumber, string HireDate,decimal Salary)
        {
            //Debugging messages:
            // To identify that this method is running
            // To identify the inputs provided from the form

            
            
            Debug.WriteLine("Create method is working");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(Salary);

            Teacher newTeacher = new Teacher();
            newTeacher.TeacherFname= TeacherFname;
            newTeacher.TeacherLname= TeacherLname;
            newTeacher.EmployeeNumber= EmployeeNumber;
            newTeacher.HireDate = Convert.ToDateTime(HireDate);
            newTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(newTeacher);
            
            return RedirectToAction("List");

        }

        /// <summary>
        /// Routes to a dynamically generated "Update" Page , gathers the current data of a teacher from the database and display it on that page.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the  teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5 -> dynamically generetaed webpage(view) where the user can update the information of the selected teacher</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            // directs to Views/Teacher/Update.cshtml
            return View(SelectedTeacher);
        }
        /// <summary>
        /// This method will direct to a webpage  asking to update a particular teacher using AJAX.
        /// </summary>
        /// <param name="id"> the id of the  selected teacher that  need to be updated </param>
        /// <returns> a view with the updated  teacher information</returns>
        /// <example> GET :/Teacher/Ajax_Update/{id} ->  render a web page (view) with updated teacher information</example>
        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing  teacher in the system, with new values. Send this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <param name="HireDate">The updated   hire date of the teacher.</param>
        ///  /// <param name="Salary">The updated  salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Edit/11
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Amanpreet",
        ///	"TeacherLname":"Kaur",
        ///	"EmployeeNumber":"T678",
        ///	"HireDate":"2023-05-05",
        ///	"Salary" :"50.00"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Edit(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, string HireDate, decimal Salary)
        {
            Teacher selectedTeacher = new Teacher();
            selectedTeacher.TeacherFname = TeacherFname;
            selectedTeacher.TeacherLname = TeacherLname;
            selectedTeacher.EmployeeNumber = EmployeeNumber;
            selectedTeacher.HireDate =  Convert.ToDateTime(HireDate);
            selectedTeacher.Salary = Salary ;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, selectedTeacher);
            //directs to Views/Teacher/Show/{id}
            return RedirectToAction("Show/" + id);
        }
    }
}