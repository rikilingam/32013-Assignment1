﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
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

    }
}
