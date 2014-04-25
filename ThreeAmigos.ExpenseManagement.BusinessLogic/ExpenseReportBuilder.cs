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
      
        public void SupervisorActionOnExpenseReport(int expenseid, Guid empId,string status)
        {
            exp.SupervisorActionOnExpenseReport(expenseid, empId,status);
        }

        //BELOW METHODS ARE USED FOR ACCOUNTANT
        /// <summary>
        /// Get list of expense report. Used for displaying for accountant approval
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<ExpenseReport> GetReportsByAccountant(string status)
        {
            return exp.GetReportsByAccountant(status);
        }

        /// <summary>
        /// Get list of amounts of expenses approved by each individual supervisor 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<Employee> GetExpenseReportsBySupervisor()
        {
            return exp.GetExpenseReportsBySupervisor();
        }

        /// <summary>
        /// Update status of expense report. Affect ExpenseHeader table
        /// </summary>
        /// <param name="expenseid"></param>
        /// <param name="empId"></param>
        /// <param name="status"></param>
        public void AccountantActionOnExpenseReport(int expenseid, Guid empId, string status)
        {
            exp.AccountantActionOnExpenseReport(expenseid, empId, status);
        }
    }
}
