﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class EmployeeDAL
    {
        private DataAccessFunctions daFunctions;

        public EmployeeDAL()
        {
            daFunctions = new DataAccessFunctions();
        }

        /// <summary>
        /// Gets employee profile from the employee table
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>returns an employee object</returns>
        public Employee GetEmployee(Guid id)
        {
            Employee employee = new Employee();
            DepartmentDAL departmentDAL = new DepartmentDAL();

            string query = String.Format("SELECT e.UserId, e.Firstname, e.Surname, e.DepartmentId, d.DepartmentName, e.Role FROM Employee e LEFT OUTER JOIN Department d on e.DepartmentId = d.DepartmentId  WHERE UserId='{0}'", id);
            SqlCommand cmd = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.UserId = (Guid)rdr.GetGuid(0);
                    employee.FirstName = (string)rdr["Firstname"];
                    employee.Surname = (string)rdr["Surname"];
                    employee.Dept = departmentDAL.GetDepartmentProfile(rdr["DepartmentId"] as int? ?? default(int));
                    employee.Role = (string)rdr["Role"];
                }

                daFunctions.Connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load user from employee table: " + ex.Message);
            }

            return employee;
        }
    }
}
