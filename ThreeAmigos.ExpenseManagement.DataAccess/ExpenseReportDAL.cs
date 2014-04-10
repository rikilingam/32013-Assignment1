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
            daFunctions = new DataAccessFunctions();
        }

        /// <summary>
        /// Inserts the expense report into the database
        /// </summary>
        /// <param name="expenseReport"></param>
        public void ProcessExpense(ExpenseReport expenseReport)
        {
            int newExpenseId = -1;

            newExpenseId = InsertExpenseHeader(expenseReport.CreatedById, expenseReport.CreateDate, expenseReport.DepartmentId, expenseReport.Status.ToString());

            foreach (ExpenseItem item in expenseReport.ExpenseItems)
            {
                item.ExpenseHeaderId = newExpenseId;
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
        /// <param name="expenseId">expense id from expense header</param>
        /// <param name="expenseDate">date of the expense</param>
        /// <param name="location">location of the expense</param>
        /// <param name="description">reason for the expense</param>
        /// <param name="amount">amount in original currency</param>
        /// <param name="currency">currency of purchase</param>
        /// <param name="audAmount">amount converted to AUD</param>
        /// <param name="receiptFileName">name of the file</param>
        private void InsertExpenseItem(int expenseId, DateTime expenseDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName)
        {

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

        public List<ExpenseReport> GetReportSummaryByConsultant(Guid id)
        {
            List<ExpenseReport> expenseReports = new List<ExpenseReport>();

            string query = String.Format("SELECT ExpenseId, CreateDate, Status, ItemId, ExpenseDate, Location, Description, AudAmount, ReceiptFileName FROM ExpenseItem i INNER JOIN ExpenseHeader h ON i.ExpenseHeaderId = h.ExpenseId WHERE h.CreatedById ='{0}'", id);
            daFunctions.Command.CommandText = query;

            try
            {
                daFunctions.Connection.Open();
                SqlDataReader rdr = daFunctions.Command.ExecuteReader();

                while (rdr.Read())
                {
                    ExpenseReport report = new ExpenseReport();
                    ExpenseItem item = new ExpenseItem();

                    report.ExpenseId = rdr["ExpenseId"] as int? ?? default(int);
                    report.CreateDate = (DateTime)rdr["CreateDate"];
                    report.Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), (string)rdr["Status"]);

                    item.ItemId = rdr["ItemId"] as int? ?? default(int);
                    item.ExpenseHeaderId = report.ExpenseId;
                    item.ExpenseDate = (DateTime)rdr["ExpenseDate"];
                    item.Location = (string)rdr["Location"];
                    item.Description = (string)rdr["Description"];
                    item.AudAmount = rdr["AudAmount"] as double? ?? default(double);
                    item.ReceiptFileName = (string)rdr["ReceiptFileName"];

                    report.ExpenseItems.Add(item);
                    expenseReports.Add(report);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method GetReportSummaryByConsultant: " + ex.Message);
            }

            return expenseReports;
        }



        public void SupervisorUpdateReport()
        {
        }

        public void AccountsUpdateReport()
        {

        }

    }
}
