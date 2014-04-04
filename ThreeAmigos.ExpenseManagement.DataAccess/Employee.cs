using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class Employee
    {
        public int FetchUserId(string username)
        {

            int employeeId;
            Guid userId;
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(connection);
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select UserId from aspnet_Users where Username='" + username + "'";
                userId = Guid.Parse(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "Select EmployeeId from employee where UserId='" + userId + "'";
                employeeId = Convert.ToInt32(cmd.ExecuteScalar());
                return employeeId;
            }
            catch (Exception e)
            {
                return (Convert.ToInt32(e.Message));
            }
        }
    }
}
