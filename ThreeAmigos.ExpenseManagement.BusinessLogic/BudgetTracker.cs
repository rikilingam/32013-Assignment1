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

        public decimal BudgetAmount
        {
            get { return budgetAmount; }
        }

        // setups the budget tracker to track departments budget
        public void DepartmentBudget(decimal deptBudget, int deptId)
        {
            budgetAmount = deptBudget;
            totalExpenseAmount = spendTracker.TotalExpenseAmountByDept(deptId, DateTime.Now.Month);
        }

        // setups the budget tracker to track company budget
        public void CompanyBudget()
        {
            budgetAmount = decimal.Parse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"]);
            totalExpenseAmount = spendTracker.TotalExpenseAmountByCompany(DateTime.Now.Month);
        }

        public decimal RemainingAmount
        {
            get
            {
                return budgetAmount - totalExpenseAmount;
            }
        }

        //Checks if the budget will be exceeded if additional expense amount is approved
        public bool IsBudgetExceeded(decimal amount)
        {
            if ((budgetAmount - (totalExpenseAmount + amount)) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
