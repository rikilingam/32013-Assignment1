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

        public int fetchUserId(string username)
        {
            return emp.fetchUserId(username);
        }
    }
}
