using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.DataAccess;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
   public class BudgetTracker
    {
         ExpenseReportDAL exp = new ExpenseReportDAL();

        public double SumOfExpenseApproved(int DeptId)
        {
            return exp.SumOfExpenseApproved(DeptId);
        }

        // Calculates the budget remaining in a department
        public double CalculateRemainingBudget(double budget, double totalSpent)
        {
            return budget - totalSpent;
        }
    }
}
