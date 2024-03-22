using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;



namespace C_Cumulative1.Models
{
    public class SchoolDbContext
    {
        //Fields required to connect with the database 
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get {
                return
                  "server = " + Server
                + "; user = " + User
                + "; database = " + Database
                + "; port = " + Port
                + "; password = " + Password;       
            }
        }
        /// <summary>
        /// This method creates  object of MySqlConnection class  which  references to a particluar connection to the  school database or we can 
        /// say this mehtod establishes a particular connection to the school database
        /// </summary>
        /// <returns>
        /// returns an object of a MySqlConnection class 
        /// </returns>
        /// <example>
        /// private SchoolDbContext School= new SchoolDbContext();
        /// MySqlConnection conn= School.AccessDatabase();
        /// </example>

        public MySqlConnection AccessDatabase()
        {    
           //Instiate an object of MySqlConnection class 
            return new MySqlConnection(ConnectionString);
        }





    }
}