using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class EmployeeDAL:BaseDataAccess
    {
        //public int FetchUserId(string username)
        //{
        //    int employeeId;
        //    Guid userId;
        //    try
        //    {
        //        string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
        //        SqlConnection con = new SqlConnection(connection);
        //        con.Open();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        // 
        //        cmd.CommandText = "Select UserId from aspnet_Users where Username='" + username + "'";
        //        userId = Guid.Parse(cmd.ExecuteScalar().ToString());
        //        cmd.CommandText = "Select EmployeeId from employee where UserId='" + userId + "'";
        //        employeeId = Convert.ToInt32(cmd.ExecuteScalar());
        //        return employeeId;
        //    }
        //    catch (Exception e)
        //    {
        //        return (Convert.ToInt32(e.Message));
        //    }
        //}


        /// <summary>
        /// Gets employee profile from the employee table
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>list with employee profile</returns>
        public Employee GetEmployee(Guid id)
        {
            Employee employee = new Employee();

            SqlConnection conn = new SqlConnection(connString);
            string query = String.Format("SELECT e.UserId, e.Firstname, e.Surname, e.DepartmentId, d.DepartmentName, e.Role FROM Employee e LEFT OUTER JOIN Department d on e.DepartmentId = d.DepartmentId  WHERE UserId='{0}'", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            
            try
            {
                conn.Open();                
                
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.UserId = (Guid)rdr.GetGuid(0);
                    employee.FirstName = (string)rdr["Firstname"];
                    employee.Surname =(string)rdr["Surname"];
                    employee.DepartmentId = (int)rdr["DepartmentId"];
                    employee.DepartmentName = (string)rdr["DepartmentName"];
                    employee.Role = (string)rdr["Role"];
                }
                
                conn.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load user from employee table: " + ex.Message);

            }

            return employee;
        }
    }
}
