using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class ExpenseReportBuilder
    {
        public ExpenseReport expenseReport;
        ExpenseReportDAL exp = new ExpenseReportDAL();

        public ExpenseReportBuilder()
        {
            expenseReport = new ExpenseReport();
        }

        /// <summary>
        /// Adds expense items to expenseItems list in ExpenseReport
        /// </summary>
        /// <param name="item">expense item</param>
        public void AddExpenseItem(ExpenseItem item)
        {
            expenseReport.ExpenseItems.Add(item);
            expenseReport.ExpenseTotal += item.AudAmount;
        }

        /// <summary>
        /// Submits the expense report for the consultant
        /// </summary>
        public void SubmitExpenseReport()
        {
            expenseReport.Status = ReportStatus.Submitted;
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            expenseReportDAL.ProcessExpense(expenseReport);
        }


        //BELOW METHODS ARE USED FOR SUPERVISORS FUNCTIONS
        public List<ExpenseReport> GetReportsBySupervisor(int id,string status)
        {
            return exp.GetReportsBySupervisor(id,status);
        }
       
        public double SumOfExpenseApproved(int id)
        {
            return exp.SumOfExpenseApproved(id);
        }

        // Ccalculates the budget remaining in a department
        public double CalculateRemainingBudget(double budget, double totalSpent)
        {
            return budget - totalSpent;
        }


        public void SupervisorActionOnExpenseReport(int expenseid, Guid empId,string status)
        {
            exp.SupervisorActionOnExpenseReport(expenseid, empId,status);
        }

        public string GetFileName(int expenseId)
        {
          return exp.GetFileName(expenseId);
        }
       
    }
}
