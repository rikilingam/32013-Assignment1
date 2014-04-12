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
        //private DataAccessFunctions daFunctions;

        public ExpenseReportDAL()
        {

        }

        /// <summary>
        /// Inserts the expense report into the database
        /// </summary>
        /// <param name="expenseReport"></param>
        public void ProcessExpense(ExpenseReport expenseReport)
        {

            expenseReport.ExpenseId = InsertExpenseHeader(expenseReport.CreatedById, expenseReport.CreateDate, expenseReport.DepartmentId, expenseReport.Status.ToString());

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

        public List<ExpenseReport> GetReportSummaryByConsultant(Guid id, string status)
        {
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();
            EmployeeDAL employeeDAL = new EmployeeDAL();

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            string query = String.Format("SELECT ExpenseId, CreateDate, CreatedById, ApprovedById, ProcessedById, Status FROM ExpenseHeader WHERE CreatedById ='{0}' AND status LIKE'{1}' ", id, status);
            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();

                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    ExpenseReport report = new ExpenseReport();
                    ExpenseItem item = new ExpenseItem();
                    Employee createdBy = new Employee();
                    Employee approvedBy = new Employee();
                    Employee processedBy = new Employee();

                    report.ExpenseId = rdr["ExpenseId"] as int? ?? default(int);
                    report.CreateDate = (DateTime)rdr["CreateDate"];
                    report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr["Status"]);

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
            string query = String.Format("SELECT ExpenseHeaderId,ItemId, ExpenseDate, Location, Description, AudAmount, ReceiptFileName FROM ExpenseItem WHERE ExpenseHeaderId={0}", expenseid);

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
                item.AudAmount = rdr["AudAmount"] as double? ?? default(double);
                item.ReceiptFileName = (string)rdr["ReceiptFileName"];

                expenseItems.Add(item);
            }

            daFunctions.Connection.Close();
            return expenseItems;
        }

        //public DataSet GetReportsByConsultant(Guid id, string status)
        //{
        //    DataSet expenseReports = new DataSet();

        //    string query = String.Format("SELECT ExpenseId, CreateDate, Status, ItemId, ExpenseDate, Location, Description, AudAmount, ReceiptFileName FROM ExpenseItem i INNER JOIN ExpenseHeader h ON i.ExpenseHeaderId = h.ExpenseId WHERE h.CreatedById ='{0}' AND status LIKE'{1}' ", id, status);

        //    daFunctions.Command.CommandText = query;

        //    SqlDataAdapter adapter = new SqlDataAdapter(query,daFunctions.Connection);

        //    try
        //    {
        //        adapter.Fill(expenseReports);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("There was a problem running method GetReportSummaryByConsultant: " + ex.Message);
        //    }

        //    return expenseReports;
        //}


        public void SupervisorUpdateReport()
        {

        }

        public void AccountsUpdateReport()
        {

        }

        public List<ExpenseReport> GetReportSummaryBySupervisor(int id)
        {
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();
          //  EmployeeDAL employeeDAL = new EmployeeDAL();

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            string query = String.Format("SELECT ExpenseId,CreateDate,CreatedById,Status,DepartmentId FROM ExpenseHeader WHERE DepartmentId ='{0}' ", id);
            daFunctions.Command = new SqlCommand(query, daFunctions.Connection);

            try
            {
                daFunctions.Connection.Open();                
                SqlDataReader rdr = daFunctions.Command.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseReport report = new ExpenseReport();                   
                    report.ExpenseId = Convert.ToInt32(rdr[0]);
                    report.CreateDate = Convert.ToDateTime(rdr[1]);
                    report.CreatedById = (Guid)(rdr[2]);
                    report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr[3]);
                    report.DepartmentId =(int) rdr[4];                 
                    report.ExpenseItems = GetExpenseItemsByExpenseId(report.ExpenseId);
                    expenseReports.Add(report);
                  }                
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method GetReportSummaryBySupervisor: " + ex.Message);
            }
            daFunctions.Connection.Close();
            return expenseReports;

        }

    }
}
