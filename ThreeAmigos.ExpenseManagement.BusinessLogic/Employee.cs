using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }

        public Employee()
        {
            EmployeeId = -1;
            FirstName = "";
            Surname = "";
            DepartmentId = -1;
            DepartmentName = "";
            Role = "";


        }

        /// <summary>
        /// Constructor loads employee from database where GUID is known
        /// </summary>
        /// <param name="id">guid id of the employee</param>
        public Employee(Guid id)
        {
            EmployeeDAL emp = new EmployeeDAL();
            
            List<string> employeeProfile = emp.GetEmployeeProfile(id);

            UserId = new Guid(employeeProfile[0]);
            FirstName = employeeProfile[1];
            Surname = employeeProfile[2];
            DepartmentId = Int32.Parse(employeeProfile[3]);
            DepartmentName = employeeProfile[4];
            Role = employeeProfile[5];

        }
        //DataAccess.Employee emp = new DataAccess.Employee();

        //public int FetchUserId(string username)
        //{
        //    return emp.FetchUserId(username);
        //}
    }
}
