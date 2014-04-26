using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;


namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class SpendTrackerDAL
    {

        //Get total of all expenses for a supervisor for a month approved by accounts
        public decimal TotalExpenseAmountBySupervisor(int supervisorId, int month)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.ApprovedById={0} AND h.Status='{1}' AND DATEPART(month,ProcessedDate)={2}", supervisorId, ReportStatus.ApprovedByAccounts, month);

            return GetExpenseTotal(query);        
        }

        //Get total amount of expenses for a department for a month where approved by supervisor and accounts
        public decimal TotalExpenseAmountByDept(int deptId, int month)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.DepartmentId={0} AND h.Status in ('{1}','{2}') AND (DATEPART(month,ProcessedDate)={3} OR DATEPART(month,ApprovedDate)={3})", deptId, ReportStatus.ApprovedByAccounts, ReportStatus.ApprovedBySupervisor, month);

            return GetExpenseTotal(query);
        }

        //Get total amount for a department which is approved by accounts
        public decimal TotalExpenseAmountByDeptProcessed(int deptId, int month)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.DepartmentId={0} AND h.Status = '{1}' AND DATEPART(month,ProcessedDate)={2}", deptId, ReportStatus.ApprovedByAccounts, month);

            return GetExpenseTotal(query);
        }

        //Get total amount for the company for a month
        public decimal TotalExpenseAmountByCompany(int month)
        {         
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.Status= '{0}' AND DATEPART(month,ProcessedDate)={1}", ReportStatus.ApprovedByAccounts, month);
         
            return GetExpenseTotal(query);
        }

        //Retrieves the expense total from the database with provided query
        private decimal GetExpenseTotal(string query)
        {
            decimal totalExpense = 0;
            DataAccessFunctions daFunctions = new DataAccessFunctions();

            daFunctions.Command.CommandText = query;

            try
            {
                daFunctions.Connection.Open();
                totalExpense = daFunctions.Command.ExecuteScalar() as decimal? ?? default(decimal);
                daFunctions.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to execute method GetExpenseTotal: " + ex.Message);
            }

            return totalExpense;
        }
    }
}
