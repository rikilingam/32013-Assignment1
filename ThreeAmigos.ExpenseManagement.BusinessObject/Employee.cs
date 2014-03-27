using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
    public class Employee
    {
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public int departmentId { get; set; }
        public string role { get; set; }
        public int userId { get; set; }        
    }
}
