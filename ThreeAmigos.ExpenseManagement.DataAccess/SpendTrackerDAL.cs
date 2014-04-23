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

        public decimal TotalExpenseAmountBySupervisor(int supervisorId)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.ApprovedById={0} AND h.Status='{1}'", supervisorId, ReportStatus.ApprovedByAccountant);

            return GetExpenseTotal(query);        
        }

        public decimal TotalExpenseAmountByDept(int deptId)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.DepartmentId={0} AND h.Status in ('{1}','{2}')", deptId, ReportStatus.ApprovedByAccountant, ReportStatus.ApprovedBySupervisor);

            return GetExpenseTotal(query);
        }

        public decimal TotalExpenseAmountByCompany()
        {
         
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.Status={0}", ReportStatus.ApprovedByAccountant);
         
            return GetExpenseTotal(query);
        }

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
