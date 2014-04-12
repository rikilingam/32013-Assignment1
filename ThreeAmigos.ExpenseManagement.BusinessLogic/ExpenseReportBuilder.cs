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
    }
}
