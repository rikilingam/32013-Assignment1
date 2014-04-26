
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
    public class ExpenseReportDAL
    {

        /// <summary>
        /// Inserts the expense report into the database
        /// </summary>
        /// <param name="expenseReport">Expense report object</param>
        public void ProcessExpense(ExpenseReport expenseReport)
        {
            expenseReport.ExpenseId = InsertExpenseHeader(expenseReport.CreatedBy.UserId, expenseReport.CreateDate, expenseReport.ExpenseToDept.DepartmentId, expenseReport.Status.ToString());

            foreach (ExpenseItem item in expenseReport.ExpenseItems)
            {
                item.ExpenseHeaderId = expenseReport.ExpenseId;
                InsertExpenseItem(item.ExpenseHeaderId, item.ExpenseDate, item.Location, item.Description, item.Amount, item.Currency, item.AudAmount, item.ReceiptFileName);
            }
        }

        
        /// <summary>
        /// Inserts the expense header into the database
        /// </summary>
        /// <param name="createdById">Employee created the expense</param>
        /// <param name="createDate">The date the expense report was created</param>
        /// <param name="departmentId">Department the expense report is allocated to</param>
        /// <param name="status">Status of the expense report</param>
        /// <returns>Returns the expense header id</returns>
        private int InsertExpenseHeader(Guid createdById, DateTime createDate, int departmentId, string status)
        {
            int expenseId = -1; // store the returned value of the expenseId post insert of new record

            DataAccessFunctions daFunctions = new DataAccessFunctions();                        
            daFunctions.Command.CommandType = CommandType.StoredProcedure;

            try
            {
                daFunctions.Command.CommandText = "AddExpenseHeader";
                daFunctions.Command.CommandType = CommandType.StoredProcedure;

                //Parameters for the expense header
                daFunctions.Command.Parameters.AddWithValue("@CreatedById", createdById);
                daFunctions.Command.Parameters.AddWithValue("@CreateDate", createDate);
                daFunctions.Command.Parameters.AddWithValue("@DepartmentId", departmentId);
                daFunctions.Command.Parameters.AddWithValue("@Status", status);

                // Will return the value of expense Id
                daFunctions.Command.Parameters.Add("@Id", SqlDbType.Int);
                daFunctions.Command.Parameters["@Id"].Direction = ParameterDirection.Output;

                daFunctions.Connection.Open();
                daFunctions.Command.ExecuteNonQuery();

                expenseId = Convert.ToInt32(daFunctions.Command.Parameters["@Id"].Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting the expense header in database: " + ex.Message);
            }
            finally
            {
                daFunctions.Connection.Close();
            }

            return expenseId;
        }


        /// <summary>
        /// Inserts expense items into the database
        /// </summary>
        /// <param name="expenseId">Expense header Id</param>
        /// <param name="expenseDate">Date of the transaction</param>
        /// <param name="location">Location of the transaction</param>
        /// <param name="description">Reason for transaction</param>
        /// <param name="amount">Amount in original currency</param>
        /// <param name="currency">Currency of the transaction</param>
        /// <param name="audAmount">Amount in AUD</param>
        /// <param name="receiptFileName">The new file name of the receipt</param>
        private void InsertExpenseItem(int expenseId, DateTime expenseDate, string location, string description, decimal amount, string currency, decimal audAmount, string receiptFileName)
        {
            DataAccessFunctions daFunctions = new DataAccessFunctions();
            daFunctions.Command.CommandType = CommandType.StoredProcedure;
            daFunctions.Command.CommandText = "AddExpenseItem";
            
            //parameters for the expense items
            daFunctions.Command.Parameters.AddWithValue("@ExpenseHeaderId", expenseId);

            daFunctions.Command.Parameters.AddWithValue("@ExpenseDate", expenseDate);
            daFunctions.Command.Parameters.AddWithValue("@Location", location);
            daFunctions.Command.Parameters.AddWithValue("@Description", description);
            daFunctions.Command.Parameters.AddWithValue("@Amount", amount);
            daFunctions.Command.Parameters.AddWithValue("@Currency", currency);
            daFunctions.Command.Parameters.AddWithValue("@AudAmount", audAmount);
            daFunctions.Command.Parameters.AddWithValue("@ReceiptFileName", receiptFileName);

            try
            {
                daFunctions.Connection.Open();
                daFunctions.Command.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting expense item to database: " + ex.Message);
            }
            finally
            {
                daFunctions.Connection.Close();
            }
        }


        /// <summary>
        /// Retrieves the expense reports from the database with given query
        /// </summary>
        /// <param name="query">sql query</param>
        /// <returns></returns>
        private List<ExpenseReport> GetReportsFromDatabase(string query)
        {
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();
            EmployeeDAL employeeDAL = new EmployeeDAL();
            DepartmentDAL departmentDAL = new DepartmentDAL();

            DataAccessFunctions daFunctions = new DataAccessFunctions();

            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();

                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    ExpenseReport report = new ExpenseReport();
                    Employee createdBy = new Employee();
                    Employee approvedBy = new Employee();
                    Employee processedBy = new Employee();
                    decimal expenseTotal;

                    report.ExpenseId = rdr["ExpenseId"] as int? ?? default(int);
                    report.DepartmentId = rdr["DepartmentId"] as int? ?? default(int);
                    report.CreateDate = (DateTime)rdr["CreateDate"];
                    report.ExpenseToDept = departmentDAL.GetDepartmentProfile(rdr["DepartmentId"] as int? ?? default(int));
                    report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr["Status"]);
                    report.CreatedBy = employeeDAL.GetEmployee(rdr["CreatedById"] as Guid? ?? default(Guid));
                    report.ApprovedBy = employeeDAL.GetEmployee(rdr["ApprovedById"] as Guid? ?? default(Guid));
                    report.ProcessedBy = employeeDAL.GetEmployee(rdr["ProcessedById"] as Guid? ?? default(Guid));
                    report.ApprovedDate = rdr["ApprovedDate"] as DateTime? ?? default(DateTime);
                    report.ExpenseItems = GetExpenseItemsByExpenseId(report.ExpenseId, out expenseTotal);
                    report.ExpenseTotal = expenseTotal;
                    expenseReports.Add(report);
                }

                daFunctions.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem retrieving expense reports: " + ex.Message);
            }

            return expenseReports;

        }

        /// <summary>
        /// Private method to get the individual expense items for a expense report
        /// </summary>
        /// <param name="expenseid">Expense Header Id</param>
        /// <param name="expenseTotal">output the total of the expense report</param>
        /// <returns>Expense items</returns>
        private List<ExpenseItem> GetExpenseItemsByExpenseId(int expenseid, out decimal expenseTotal)
        {
            expenseTotal = 0;

            List<ExpenseItem> expenseItems = new List<ExpenseItem>();

            DataAccessFunctions daFunctions = new DataAccessFunctions();

            string query = String.Format("SELECT * FROM ExpenseItem WHERE ExpenseHeaderId={0}", expenseid);

            try
            {
                daFunctions.Connection.Open();
                daFunctions.Command.CommandText = query;
                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    ExpenseItem item = new ExpenseItem();

                    item.ExpenseHeaderId = rdr["ExpenseHeaderId"] as int? ?? default(int);
                    item.ItemId = rdr["ItemId"] as int? ?? default(int);
                    item.ExpenseDate = (DateTime)rdr["ExpenseDate"];
                    item.Location = (string)rdr["Location"];
                    item.Description = (string)rdr["Description"];
                    item.Currency = (string)rdr["Currency"];
                    item.Amount = rdr["Amount"] as decimal? ?? default(decimal);
                    item.AudAmount = rdr["AudAmount"] as decimal? ?? default(decimal);
                    item.ReceiptFileName = (string)rdr["ReceiptFileName"];

                    expenseTotal += item.AudAmount;
                    expenseItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem retrieving expense items for expenseid: " + expenseid + ": " + ex.Message);
            }
            finally
            {
                daFunctions.Connection.Close();
            }
            return expenseItems;
        }

        /// <summary>
        /// Gets reports from database by consultants employee id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="status">status of report</param>
        /// <returns>Expense reports</returns>
        public List<ExpenseReport> GetExpenseReportsByConsultant(Guid id, string status)
        {
            string query;

            if (status == "Pending")
            {
                query = string.Format("SELECT * FROM ExpenseHeader WHERE CreatedById='{0}' and Status in ('{1}','{2}')", id, ReportStatus.ApprovedBySupervisor, ReportStatus.Submitted);
            }
            else
            {
                query = string.Format("SELECT * FROM ExpenseHeader WHERE CreatedById='{0}' and Status LIKE '{1}'", id, status);
            }            

            return GetReportsFromDatabase(query);
        }

        /// <summary>
        /// Get the all expense reports for a department by status
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <param name="status">The status of the report</param>
        /// <returns></returns>
        public List<ExpenseReport> GetReportsByDepartment(int id, string status)
        {
            string query = string.Format("SELECT * FROM ExpenseHeader WHERE  DepartmentId ={0} and Status ='{1}'", id, status);
            return GetReportsFromDatabase(query);
        }


        /// <summary>
        /// Gets all reports by status
        /// </summary>
        /// <param name="status">Status of the report</param>
        /// <returns></returns>
        public List<ExpenseReport> GetReportsByStatus(string status)
        {
            string query = string.Format("SELECT * FROM ExpenseHeader WHERE Status ='{0}'", status);
            return GetReportsFromDatabase(query);
        }

        /// <summary>
        /// Updates the expense header for the supervisor role
        /// </summary>
        /// <param name="expenseId">Expense Id to update</param>
        /// <param name="empId">The employee id of the supervisor</param>
        /// <param name="status">New status of the report</param>
        public void SupervisorActionOnExpenseReport(int expenseId, Guid empId, string status)
        {
            string query = "update ExpenseHeader set ApprovedById=@ApprovedById,  ApprovedDate=@ApprovedDate,Status=@Status where ExpenseId='" + expenseId + "'";
            //  where Username='" + username + "'";                                  
            DataAccessFunctions daFunctions = new DataAccessFunctions();
            try
            {
                daFunctions.Connection.Open();

                daFunctions.Command = new SqlCommand(query, daFunctions.Connection);
                daFunctions.Command.Parameters.AddWithValue("@ApprovedById", empId);
                daFunctions.Command.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                daFunctions.Command.Parameters.AddWithValue("@Status", status);
                daFunctions.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem updating the approval status of the expense: " + ex.Message);
            }
            finally
            {
                daFunctions.Connection.Close();
            }
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
        public List<Employee> GetExpenseReportsBySupervisor()
        {
            List<Employee> employees = new List<Employee>();
            EmployeeDAL employeeDAL = new EmployeeDAL();
            DataAccessFunctions daFunctions = new DataAccessFunctions();

            //string query = string.Format("SELECT H.ApprovedById AS SupervisorId, SUM(I.AudAmount) AS AmountApproved FROM ExpenseItem I LEFT OUTER JOIN ExpenseHeader H ON I.ExpenseHeaderId = H.ExpenseId WHERE H.Status ='ApprovedByAccounts' GROUP BY H.ApprovedById");
            string query = string.Format("SELECT ApprovedById, COUNT(ExpenseId) AS AmountApproved FROM ExpenseHeader WHERE Status ='ApprovedByAccounts' GROUP BY ApprovedById");
            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();
                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp = employeeDAL.GetEmployee(rdr["ApprovedById"] as Guid? ?? default(Guid));
                    emp.AmountApproved = rdr["AmountApproved"] as int? ?? default(int);
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

        /// <summary>
        /// Updates to the status of the expense report for the accounts role
        /// </summary>
        /// <param name="expenseId">Expense Id of the report to update</param>
        /// <param name="empId">Employee Id of the accounts person</param>
        /// <param name="status">New status</param>
        public void AccountantActionOnExpenseReport(int expenseId, Guid empId, string status)
        {
            string query = "update ExpenseHeader set ProcessedById=@ProcessedById,  ProcessedDate=@ProcessedDate,Status=@Status where ExpenseId='" + expenseId + "'";

            DataAccessFunctions daFunctions = new DataAccessFunctions();

            try
            {
                daFunctions.Connection.Open();

                daFunctions.Command = new SqlCommand(query, daFunctions.Connection);
                daFunctions.Command.Parameters.AddWithValue("@ProcessedById", empId);
                daFunctions.Command.Parameters.AddWithValue("@ProcessedDate", DateTime.Now);
                daFunctions.Command.Parameters.AddWithValue("@Status", status);
                daFunctions.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem updating the status of the expense: " + ex.Message);
            }
            finally
            {
                daFunctions.Connection.Close();
            }
        }
    }
}
