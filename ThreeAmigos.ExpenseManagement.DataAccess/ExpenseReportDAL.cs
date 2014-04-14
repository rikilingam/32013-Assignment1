
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
        private DataAccessFunctions daFunctions;

        public ExpenseReportDAL()
        {

        }

        /// <summary>
        /// Inserts the expense report into the database
        /// </summary>
        /// <param name="expenseReport"></param>
        public void ProcessExpense(ExpenseReport expenseReport)
        {
            expenseReport.ExpenseId = InsertExpenseHeader(expenseReport.CreatedBy.UserId, expenseReport.CreateDate, expenseReport.DepartmentId, expenseReport.Status.ToString());

            foreach (ExpenseItem item in expenseReport.ExpenseItems)
            {
                item.ExpenseHeaderId = expenseReport.ExpenseId;
                InsertExpenseItem(item.ExpenseHeaderId, item.ExpenseDate, item.Location, item.Description, item.Amount, item.Currency, item.AudAmount, item.ReceiptFileName);
            }
        }

        /// <summary>
        /// Inserts the expense header using a storeprocedure AddExpenseHeader
        /// </summary>
        /// <returns>Returns the expense id</returns>
        private int InsertExpenseHeader(Guid createdById, DateTime createDate, int departmentId, string status)
        {
            int expenseId = -1; // store the returned value of the expenseId post insert of new record

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = daFunctions.Connection;
                cmd.CommandText = "AddExpenseHeader";
                cmd.CommandType = CommandType.StoredProcedure;

                //Parameters for the expense header
                cmd.Parameters.AddWithValue("@CreatedById", createdById);
                cmd.Parameters.AddWithValue("@CreateDate", createDate);
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                cmd.Parameters.AddWithValue("@Status", status);

                // Will return the value of expense Id
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

                daFunctions.Connection.Open();
                cmd.ExecuteNonQuery();
                daFunctions.Connection.Close();

                expenseId = Convert.ToInt32(cmd.Parameters["@Id"].Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting the expense header: " + ex.Message);

            }

            return expenseId;
        }

        /// <summary>
        /// Inserts the each expense item into the database,
        /// with a foreign key of expenseId
        /// </summary>
        private void InsertExpenseItem(int expenseId, DateTime expenseDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName)
        {
            DataAccessFunctions daFunctions = new DataAccessFunctions();
            string query = String.Format("INSERT INTO ExpenseItem (ExpenseHeaderId, ExpenseDate, Location, Description, Amount, Currency,AudAmount,ReceiptFileName) VALUES({0},'{1}','{2}','{3}',{4},'{5}',{6},'{7}')", expenseId, expenseDate, location, description, amount, currency, audAmount, receiptFileName);
            SqlCommand cmd = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();
                cmd.ExecuteNonQuery();
                daFunctions.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting expense item: " + ex.Message);
            }

        }

        public List<ExpenseReport> GetExpenseReportByConsultant(Guid id, string status)
        {
            string query = String.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE CreatedById='{0}' and Status LIKE '{1}'", id, status);

            return GetReportsFromDatabase(query);
        }

        private List<ExpenseReport> GetReportsFromDatabase(string query)
        {
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();
            EmployeeDAL employeeDAL = new EmployeeDAL();

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

                    report.ExpenseId = rdr["ExpenseId"] as int? ?? default(int);
                    report.CreateDate = (DateTime)rdr["CreateDate"];
                    report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr["Status"]);
                    report.CreatedBy = employeeDAL.GetEmployee(rdr["CreatedById"] as Guid? ?? default(Guid));
                    report.ApprovedBy = employeeDAL.GetEmployee(rdr["ApprovedById"] as Guid? ?? default(Guid));
                    report.ProcessedBy = employeeDAL.GetEmployee(rdr["ProcessedById"] as Guid? ?? default(Guid));
                    report.ExpenseItems = GetExpenseItemsByExpenseId(report.ExpenseId);
                    expenseReports.Add(report);
                }

                daFunctions.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method GetReportSummaryByConsultant: " + ex.Message);
            }

            return expenseReports;

        }

        public List<ExpenseItem> GetExpenseItemsByExpenseId(int expenseid)
        {

            List<ExpenseItem> expenseItems = new List<ExpenseItem>();

            DataAccessFunctions daFunctions = new DataAccessFunctions();

            string query = String.Format("SELECT ExpenseHeaderId,ItemId, ExpenseDate, Location, Description,Amount,AudAmount, ReceiptFileName FROM ExpenseItem WHERE ExpenseHeaderId={0}", expenseid); // Added Amount field just for testing

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
                item.Amount = Convert.ToDouble(rdr["Amount"]);//as double? ?? default(double);  // for testing
                item.AudAmount = rdr["AudAmount"] as double? ?? default(double);
                item.ReceiptFileName = (string)rdr["ReceiptFileName"];

                expenseItems.Add(item);
            }

            daFunctions.Connection.Close();
            return expenseItems;
        }

        public DataSet GetReportsByConsultant(Guid id, string status)
        {
            DataSet expenseReports = new DataSet();

            string query = String.Format("SELECT ExpenseId, CreateDate, Status, ItemId, ExpenseDate, Location, Description, AudAmount, ReceiptFileName FROM ExpenseItem i INNER JOIN ExpenseHeader h ON i.ExpenseHeaderId = h.ExpenseId WHERE h.CreatedById ='{0}' AND status LIKE'{1}' ", id, status);

            daFunctions.Command.CommandText = query;

            SqlDataAdapter adapter = new SqlDataAdapter(query, daFunctions.Connection);

            try
            {
                adapter.Fill(expenseReports);
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method GetReportSummaryByConsultant: " + ex.Message);
            }

            return expenseReports;
        }





        // Below are the methods used by supervisor
        public List<ExpenseReport> GetReportSummaryBySupervisor(int id)
        {
            string query = string.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE DepartmentId ='{0}' and Status ='{1}' ", id, ReportStatus.Submitted);
            return GetReportsFromDatabase(query);
        }
        public double SumOfExpenseApproved(int id)
        {
            double totalExpenseApproved = 0;
            string query = string.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE DepartmentId ='{0}' and Status ='{1}' ", id, ReportStatus.ApproveBySupervisor);
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();
            expenseReports = GetReportsFromDatabase(query);
            totalExpenseApproved = SumOfExpenseItem(expenseReports);

            //EmployeeDAL employeeDAL = new EmployeeDAL();
            //DataAccessFunctions daFunctions = new DataAccessFunctions();
            //daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            //try
            //{
            //    daFunctions.Connection.Open();
            //    SqlDataReader rdr = daFunctions.Command.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        ExpenseReport report = new ExpenseReport();
            //        Employee createdBy = new Employee();
            //        Employee approvedBy = new Employee();
            //        Employee processedBy = new Employee();

            //        report.ExpenseId = rdr["ExpenseId"] as int? ?? default(int);
            //        report.CreateDate = (DateTime)rdr["CreateDate"];
            //        report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr["Status"]);
            //        report.CreatedBy = employeeDAL.GetEmployee(rdr["CreatedById"] as Guid? ?? default(Guid));
            //        report.ApprovedBy = employeeDAL.GetEmployee(rdr["ApprovedById"] as Guid? ?? default(Guid));
            //        report.ProcessedBy = employeeDAL.GetEmployee(rdr["ProcessedById"] as Guid? ?? default(Guid));
            //        report.ExpenseItems = GetExpenseItemsByExpenseId(report.ExpenseId);
            //        expenseReports.Add(report);

            //        totalExpenseApproved = SumOfExpenseItem(expenseReports);
            //    }

            //    daFunctions.Connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("There was a problem running method GetReportSummaryByConsultant: " + ex.Message);
            //}
            return totalExpenseApproved;

        }
        public double SumOfExpenseItem(List<ExpenseReport> rep)
        {
            double sum = 0;
            foreach (var item in rep)
            {
                for (int i = 0; i < item.ExpenseItems.Count; i++)
                    sum = sum + item.ExpenseItems[i].Amount;
            }
            return sum;
        }
        public void SupervisorAddExpenseReport(int expenseId, Guid empId)
        {
            string query = "update ExpenseHeader set ApprovedById=@ApprovedById,  ApprovedDate=@ApprovedDate,Status=@Status where ExpenseId='" + expenseId + "'";
            //  where Username='" + username + "'";                                  
            DataAccessFunctions daFunctions = new DataAccessFunctions();
            daFunctions.Connection.Open();

            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);
            daFunctions.Command.Parameters.AddWithValue("@ApprovedById", empId);
            daFunctions.Command.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
            daFunctions.Command.Parameters.AddWithValue("@Status", ReportStatus.ApprovedBySupervisor.ToString());
            daFunctions.Command.ExecuteNonQuery();
            daFunctions.Connection.Close();
        }
        public void SupervisorRejectExpenseReport(int expenseId, Guid empId)
        {
            string query = "update ExpenseHeader set ApprovedById=@ApprovedById,  ApprovedDate=@ApprovedDate,Status=@Status where ExpenseId='" + expenseId + "'";

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            daFunctions.Connection.Open();

            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);
            daFunctions.Command.Parameters.AddWithValue("@ApprovedById", empId);
            daFunctions.Command.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
            daFunctions.Command.Parameters.AddWithValue("@Status", ReportStatus.RejectedBySupervisor.ToString());
            daFunctions.Command.ExecuteNonQuery();
            daFunctions.Connection.Close();
        }

        // Other Methods

        public List<ExpenseReport> GetReportsApprovedBySupervisor(int id)
        {
            string query = string.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE DepartmentId ='{0}' and Status ='{1}' ", id, ReportStatus.ApproveBySupervisor);
            return GetReportsFromDatabase(query);
        }

        public List<ExpenseReport> GetReportsRejectedBySupervisor(int id)
        {
            string query = string.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE DepartmentId ='{0}' and Status ='{1}' ", id, ReportStatus.RejectedBySupervisor);
            return GetReportsFromDatabase(query);
        }

        public List<ExpenseReport> GetReportsRejectedByAccountsStaff(int id)
        {
            string query = string.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE DepartmentId ='{0}' and Status ='{1}' ", id, ReportStatus.RejectedByAccountant);
            return GetReportsFromDatabase(query);
        }
    }
}
