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

        public double TotalExpenseAmountBySupervisor(int supervisorId)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.ApprovedById={0} AND h.Status={1}", supervisorId, ReportStatus.ApprovedByAccountant);

            return GetExpenseTotal(query);        
        }

        public double TotalExpenseAmountByDept(int deptId)
        {
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.DepartmentId={0} AND h.Status={1}", deptId, ReportStatus.ApprovedByAccountant);

            return GetExpenseTotal(query);
        }

        public double TotalExpenseAmountByCompany()
        {
         
            string query = string.Format("SELECT SUM(AudAmount) FROM ExpenseItem i LEFT OUTER JOIN ExpenseHeader h on i.ExpenseHeaderId = h.ExpenseId WHERE h.Status={0}", ReportStatus.ApprovedByAccountant);
         
            return GetExpenseTotal(query);
        }

        private double GetExpenseTotal(string query)
        {
            double totalExpense = 0;
            DataAccessFunctions daFunctions = new DataAccessFunctions();
                        
            daFunctions.Command.CommandText = query;

            totalExpense = daFunctions.Command.ExecuteScalar() as double? ?? default(double);

            return totalExpense;
        }
    }
}
