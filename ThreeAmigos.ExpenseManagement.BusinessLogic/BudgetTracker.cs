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
        private double budgetAmount;
        private double totalExpenseAmount;
        
        ExpenseReportDAL exp;

        SpendTrackerDAL spendTracker;

        public BudgetTracker()
        {
            exp = new ExpenseReportDAL();
            spendTracker = new SpendTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
        }

        public double TotalExpenseAmount
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

        public void DepartmentBudget(double deptBudget, int deptId)
        {
            budgetAmount = deptBudget;
            totalExpenseAmount = spendTracker.TotalExpenseAmountByDept(deptId);
        }

        public void CompanyBudget()
        {
            budgetAmount = double.Parse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"]);
            totalExpenseAmount = spendTracker.TotalExpenseAmountByCompany();
        }

        public double RemainingAmount()
        {
            return budgetAmount - totalExpenseAmount;
        }




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
