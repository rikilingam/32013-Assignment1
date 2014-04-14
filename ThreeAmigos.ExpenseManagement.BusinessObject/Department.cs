﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public double MonthlyBudget { get; set; }


        public Department()
        {
            DepartmentId = -1;
            DepartmentName = "";
            MonthlyBudget = -1.00;
        }
    }
}
