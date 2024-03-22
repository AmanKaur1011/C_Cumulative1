﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using C_Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace C_Cumulative1.Controllers
{    // This controller allows to  make a database connection and to access the information of  teachers table
    public class TeacherDataController : ApiController
    {
        //Instantiate an object of SchoolDbContext class which allows an access to a school database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method establish a unique  connection to School database and execute a particular query and 
        ///  provide the list of teachers from the database in response to that query 
        ///  <param name="SearchKey"> The string of characters eneterd in the search text field  that will be used  
        ///  to search for a specific teacher or  particular teachers list from the database   </param>
        /// <returns>
        /// Outputs the list of teachers by showing their first name and last name  
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/TeacherData/ListTeachers ->  (list of teachers from the database with their first name and last name)
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        // SearchKey is optional -> if there will be no SearchKey provided to the method it will display the list of all the teachers 
                                  //-> if SearchKey is  provided it will display  the list of  teachers based on the SearchKey
                                  // whereas search key is the  string of characters entered to searcvh for particular teacher or  teachers.
        public IEnumerable<Teacher> ListTeachers(string SearchKey= null)
        {
            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd= conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ',teacherlname)) like lower(@key) or salary like @key or hiredate like @key";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            
            // store the result set
           MySqlDataReader ResultSet=  cmd.ExecuteReader();

            //An empty List which is going to store the list of teachers returned from the exceuted  query
            List<Teacher> teachersList = new List<Teacher>{ };

            //loop will iterate till the last row of the result set 
            while (ResultSet.Read())
            {
                int outputid =Convert.ToInt32(ResultSet["teacherid"]);
                string outputfname = ResultSet["teacherfname"].ToString();
                string outputlname= ResultSet["teacherlname"].ToString();
                string outputemployeeno= ResultSet["employeenumber"].ToString();
                DateTime outputhiredate= Convert.ToDateTime(ResultSet["hiredate"]);
                decimal outputsalary= Convert.ToDecimal(ResultSet["salary"]);

                Teacher newTeacher = new Teacher();   //Creates an object of Teacher Class

                // assign the values returned from the executed query to the Teacher object
                newTeacher.TeacherId= outputid;
                newTeacher.TeacherFname = outputfname;
                newTeacher.TeacherLname = outputlname;
                newTeacher.EmployeeNumber = outputemployeeno;
                newTeacher.HireDate = outputhiredate;
                newTeacher.Salary = outputsalary;

                // add the Teacher object to the  teachersList List
                teachersList.Add(newTeacher);

            }

            // Close the connection
            conn.Close();


            return teachersList;

        }// end of List method


        /// <summary>
        /// This method establish a unique  connection to School database and execute the  queries and 
        ///  provide the particular teacher's information from the database 
        ///<param name="id"> the id if the selected teacher</param>
        /// <returns>
        /// Outputs the particular teacher information  with the courses that  they are teaching 
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/TeacherData/FindTeacher ->  information about the selected teacher 
        ///                                                  -> TeacherId, Teacher name, teacher hire date, teacher's salary, teacher's employee number
        ///                                                  -> and the courses taught by the teacher
        /// </example>


        [HttpGet]
         public Teacher FindTeacher(int id)
        {
            //Instantiate an object of Teacher  class
            Teacher newTeacher = new Teacher();

            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd = conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from teachers where teacherid = "+ id;
            // store the result set
            MySqlDataReader ResultSet = cmd.ExecuteReader();



           
            while (ResultSet.Read())
            {
                 int outputid = Convert.ToInt32(ResultSet["teacherid"]);
                 string outputfname = ResultSet["teacherfname"].ToString();
                 string outputlname = ResultSet["teacherlname"].ToString();
                 string  outputemployeeno = ResultSet["employeenumber"].ToString();
                 DateTime outputhiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                 decimal outputsalary = Convert.ToDecimal(ResultSet["salary"]);


                newTeacher.TeacherId = outputid;
                newTeacher.TeacherFname = outputfname;
                newTeacher.TeacherLname = outputlname;
                newTeacher.EmployeeNumber = outputemployeeno;
                newTeacher.HireDate = outputhiredate;
                newTeacher.Salary = outputsalary;

           }
            //close the first result set acquired by executing the first query

            ResultSet.Close();


           // Execute  a second query which will select the row matching to the id of the selected  teacher aquired from the first Query
            cmd.CommandText = "Select * from classes where teacherid = " + id;

            //store it in a different result set 
            MySqlDataReader ResultSet2 = cmd.ExecuteReader();

            //created an empty list of courses 
            List<string> courseList = new List<string>();

            //loop will iterate till the last row of the result set 
            while (ResultSet2.Read())
            {
             
                string outputclassname = ResultSet2["classname"].ToString();
                courseList.Add(outputclassname);

                
            }
            // assign the values  obtained from the second query to the teacher object 
            newTeacher.courses = courseList;
            return newTeacher;
           


        }// end of FindTeacher method
       




    }
}