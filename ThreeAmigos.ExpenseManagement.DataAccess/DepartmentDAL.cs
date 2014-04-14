using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    class DepartmentDAL
    {
        private DataAccessFunctions daFunctions;

        public DepartmentDAL()
        {
            daFunctions = new DataAccessFunctions();
        }

        public Department GetDepartmentProfile(int departmentId)
        {
            Department department = new Department();

            string query = String.Format("SELECT * FROM Department WHERE DepartmentId ={0}", departmentId);
            
            daFunctions.Command.CommandText = query;
            
            try
            {
                daFunctions.Connection.Open();

                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    department.DepartmentId = rdr["DepartmentId"] as int? ?? default(int);
                    department.DepartmentName = (string)rdr["DepartmentName"];
                    department.MonthlyBudget = rdr["MonthlyBudget"] as double? ?? default(double);
                }

                daFunctions.Connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load department profile: " + ex.Message);
            }

            return department;
        
        }

    }
}
