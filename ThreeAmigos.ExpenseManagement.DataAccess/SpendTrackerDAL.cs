using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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

        /// <summary>
        /// Get the total amount of expenses approved by individual supervisor, including:
        ///    - already approved by both supervisor and accountant (i.e. Status = ApprovedByAccountant)
        /// EXCLUDING:
        ///    - pending for accountant approval (i.e. Status = ApprovedBySupervisor)
        ///    - approved by supervisor BUT rejected by accountant (i.e. Status = RejectedByAccountant)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<Employee> GetSpendBySupervisors(int month)
        {
            List<Employee> employees = new List<Employee>();
            EmployeeDAL employeeDAL = new EmployeeDAL();
            DataAccessFunctions daFunctions = new DataAccessFunctions();

            string query = string.Format("SELECT H.ApprovedById AS SupervisorId, COUNT(H.ExpenseId) AS AmountApproved, SUM(I.AudAmount) AS ExpenseApproved FROM ExpenseItem I LEFT OUTER JOIN ExpenseHeader H ON I.ExpenseHeaderId = H.ExpenseId WHERE H.Status ='ApprovedByAccounts' AND DATEPART(month,ProcessedDate)={0} GROUP BY H.ApprovedById", month);
            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();
                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp = employeeDAL.GetEmployee(rdr["SupervisorId"] as Guid? ?? default(Guid));
                    emp.AmountApproved = rdr["AmountApproved"] as int? ?? default(int);
                    emp.ExpenseApproved = rdr["ExpenseApproved"] as decimal? ?? default(decimal);
                    employees.Add(emp);
                }
                daFunctions.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem retrieving expense approved by supervisor reports: " + ex.Message);
            }
            return employees;
        }


    }
}
