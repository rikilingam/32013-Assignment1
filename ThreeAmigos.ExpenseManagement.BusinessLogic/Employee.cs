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
   
       DataAccess.Employee emp = new DataAccess.Employee();
        public int FetchUserId(string username)
        {
            return emp.FetchUserId(username);
        }
    }
}
