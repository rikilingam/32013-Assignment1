using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.DataAccess;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class BudgetTracker
    {
        private decimal budgetAmount;
        private decimal totalExpenseAmount;
        
        ExpenseReportDAL exp;

        SpendTrackerDAL spendTracker;

        public BudgetTracker()
        {
            exp = new ExpenseReportDAL();
            spendTracker = new SpendTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
        }

        public decimal TotalExpenseAmount
        {
            get
            {
                return totalExpenseAmount;
            }

            set
            {
                if (value > 0)
                    totalExpenseAmount += value;
            }
        }

        public void DepartmentBudget(decimal deptBudget, int deptId)
        {
            budgetAmount = deptBudget;
            totalExpenseAmount = spendTracker.TotalExpenseAmountByDept(deptId);
        }

        public void CompanyBudget()
        {
            budgetAmount = decimal.Parse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"]);
            totalExpenseAmount = spendTracker.TotalExpenseAmountByCompany();
        }

        public decimal RemainingAmount()
        {
            return budgetAmount - totalExpenseAmount;
        }




        public decimal SumOfExpenseApproved(int DeptId)
        {
            return exp.SumOfExpenseApproved(DeptId);
        }

        // Calculates the budget remaining in a department
        public decimal CalculateRemainingBudget(decimal budget, decimal totalSpent)
        {
            return budget - totalSpent;
        }
    }
}
