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
    // This controller allows to  make a database connection and to access the information of Classes table
    public class ClassDataController : ApiController
    {

        //Instantiate an object of SchoolDbContext class which allows an access to a school database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method establish a unique  connection to School database and execute a particular query and 
        ///  provide the list of  classes  from database in response to that query 
        /// <returns>
        /// Outputs the list of classes 
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/ClassData/ListClasses ->  list of Classes from the database with class  code and  class name  )
        /// </example>

        [HttpGet]
    
        public IEnumerable<Class> ListClasses()
        {
            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd = conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from classes;";

            // store the result set
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //An empty List whiich is going to store the list of classes returned from the exceuted  query
            List<Class> classesList = new List<Class> { };
            //loop will iterate till the last row of the result set 
            while (ResultSet.Read())
            {
                int outputid = Convert.ToInt32(ResultSet["classid"]);
                string outputcode = ResultSet["classcode"].ToString();
                int outputteacherid = Convert.ToInt32(ResultSet["teacherid"]);
                DateTime outputstartdate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime outputfinishdate = Convert.ToDateTime(ResultSet["finishdate"]);
                string outputclassname = ResultSet["classname"].ToString();

                Class newClass = new Class();//Creates an object of 'Class' class

                // assign the values returned from the executed query to the Class objcet

                newClass.ClassId = outputid;
                newClass.ClassCode = outputcode;
                newClass.TeacherId = outputteacherid;
                newClass.StartDate = outputstartdate.ToShortDateString();
                newClass.FinishDate = outputfinishdate.ToShortDateString();
                newClass.ClassName = outputclassname;

                // add the Class object to the  classesList List
                classesList.Add(newClass);

            }

            // Close the connection
            conn.Close();


            return classesList;
        }// end of ListClasses method


        /// <summary>
        /// This method establish a unique  connection to School database and execute the  query and 
        ///  provide the particular Class information from the database 
        ///<param name="id"> the id if the selected Class</param>
        /// <returns>
        /// Outputs the particular Class information  
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/api/ClassData/FindClass ->  information about the selected Class 
        ///                                                  -> Class Id, Class Code, Class name,class start date, class finish date, teacherid 
        ///                                               
        /// </example>

        [HttpGet]

        public Class FindClass(int id)
        {
            // Instaantiatre an objecct 'Class' class
            Class newClass = new Class();
            //Instantiate an object of MySqlConnection class
            MySqlConnection conn = School.AccessDatabase();

            //Open a connection
            conn.Open();
            // Establish a MySql query
            MySqlCommand cmd = conn.CreateCommand();
            //Execute the Query
            cmd.CommandText = "Select * from classes where classid = " + id; 

            // store the result set
            MySqlDataReader ResultSet = cmd.ExecuteReader();

           
            //loop will iterate till the last row of the result set 
            while (ResultSet.Read())
            {
                int outputid = Convert.ToInt32(ResultSet["classid"]);
                string outputcode = ResultSet["classcode"].ToString();
                int outputteacherid = Convert.ToInt32(ResultSet["teacherid"]);
                DateTime outputstartdate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime outputfinishdate = Convert.ToDateTime(ResultSet["finishdate"]);
                string outputclassname = ResultSet["classname"].ToString();

                // assign the obtained values from the query execution  to the Class object 
                newClass.ClassId = outputid;
                newClass.ClassCode = outputcode;
                newClass.TeacherId = outputteacherid;
                newClass.StartDate = outputstartdate.ToShortDateString() ;
                newClass.FinishDate = outputfinishdate.ToShortDateString();
                newClass.ClassName = outputclassname;

               

            }

            // Close the connection
            conn.Close();


            return newClass;
        }//end of the FindClass  metjod







    }
}
