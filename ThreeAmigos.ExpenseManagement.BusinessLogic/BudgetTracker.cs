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
        private decimal totalExpenseAmountAccounts;
        
        ExpenseReportDAL exp;

        SpendTrackerDAL spendTracker;

        public BudgetTracker()
        {
            exp = new ExpenseReportDAL();
            spendTracker = new SpendTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
            totalExpenseAmountAccounts = 0;
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
            totalExpenseAmountAccounts = spendTracker.TotalExpenseAmountByDeptProcessed(deptId, DateTime.Now.Month);
        }

        // setups the budget tracker to track company budget
        public void CompanyBudget()
        {
            
            if (!decimal.TryParse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"], out budgetAmount) || budgetAmount < 0)
            {
                budgetAmount = 0;
            }
                        
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


        //Used for Accounts. The remaining budget of department after being approved by accounts (i.e. Status = ApprovedByAccounts)
        public decimal RemainingAmountAccounts
        {
            get
            {
                return budgetAmount - totalExpenseAmountAccounts;
            }
        }

        //Used for Accounts. Checks if the budget will be exceeded if additional expense amount is approved
        public bool IsBudgetExceededAccounts(decimal amount)
        {
            if ((budgetAmount - (totalExpenseAmountAccounts + amount)) < 0)
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
