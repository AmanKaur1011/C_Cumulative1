using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using C_Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace C_Cumulative1.Controllers
{
    // This controller allows to  make a database connection and to access the information of Students table
    public class StudentDataController : ApiController
    {
        //Instantiate an object of SchoolDbContext class which allows an access to a school database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method establish a unique  connection to School database and execute a particular query and 
        ///  provide the list of students from the database in response to that query 
        /// <returns>
        /// Outputs the list of students by showing their first name and last name  
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/StudentData/ListStudents ->  (list of studentss from the database with their first name and last name)
        /// </example>

        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd = conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from students;";

            // store the result set
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //An empty List which is going to store the list of students returned from the exceuted  query
            List<Student> studentList = new List<Student> { };
            //loop will iterate till the last row of the result set 
            while (ResultSet.Read())
            {
                int outputid = Convert.ToInt32(ResultSet["studentid"]);
                string outputfname = ResultSet["studentfname"].ToString();
                string outputlname = ResultSet["studentlname"].ToString();
                string outputstudentno = ResultSet["studentnumber"].ToString();
                DateTime outputenroldate = Convert.ToDateTime(ResultSet["enroldate"]);

                Student newStudent = new Student();//Creates an object of Student Class

                // assign the values returned from the executed query to the Student objcet
                newStudent.StudentId = outputid;
                newStudent.StudentFname = outputfname;
                newStudent.StudentLname = outputlname;
                newStudent.StudentNumber = outputstudentno;
                newStudent.EnrolDate = outputenroldate.ToShortDateString() ;


                // add the Student object to the  studentList List
                studentList.Add(newStudent);


            }
            return studentList;


        }//end  of ListStudents method

        /// <summary>
        /// This method establish a unique  connection to School database and execute the  query and 
        ///  provide the particular Student's information from the database 
        ///<param name="id"> the id if the selected student</param>
        /// <returns>
        /// Outputs the particular student information  
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/StudentData/FindStudent ->  information about the selected student 
        ///                                                  -> Student Id, Student name, Student enrol date, Student number
        ///                                               
        /// </example>

        [HttpGet]
        public Student FindStudent(int id)
        {
            //Instantiate an object of Student class
            Student newStudent = new Student();

            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd = conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from students where studentid = " + id;
            // store the result set
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int outputid = Convert.ToInt32(ResultSet["studentid"]);
                string outputfname = ResultSet["studentfname"].ToString();
                string outputlname = ResultSet["studentlname"].ToString();
                string outputstudentno = ResultSet["studentnumber"].ToString();
                DateTime outputenroldate = Convert.ToDateTime(ResultSet["enroldate"]);

                // assign the obtained values from the query execution  to the student object 
                newStudent.StudentId = outputid;
                newStudent.StudentFname = outputfname;
                newStudent.StudentLname = outputlname;
                newStudent.StudentNumber = outputstudentno;
                newStudent.EnrolDate = outputenroldate.ToShortDateString();// ToShortDataString() gets only the date value as string from the datetime object 
                                                                           // as in the students table there is no time mentioned so only date value is required for the  enrol date

            }
            return newStudent;
        }// end of FindStudent method
    }
}