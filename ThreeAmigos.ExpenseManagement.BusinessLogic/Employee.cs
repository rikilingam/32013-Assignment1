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
        public string SurName { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }  

        DataAccess.Employee emp = new DataAccess.Employee();
        
        public int FetchUserId(string username)
        {
            return emp.FetchUserId(username);
        }
    }
}
