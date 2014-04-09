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

        public void ProcessExpense(ExpenseReport expenseReport)
        {
            int newExpenseId = -1;

            newExpenseId = InsertExpenseHeader(expenseReport.CreatedById,expenseReport.CreateDate,expenseReport.DepartmentId,expenseReport.Status.ToString());

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
        public int InsertExpenseHeader(Guid createdById, DateTime createDate, int departmentId, string status)
        {
            int expenseId = -1; // store the returned value of the expenseId post insert of new record
            //SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = daFunctions.ConnectionString;
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

                daFunctions.ConnectionString.Open();
                cmd.ExecuteNonQuery();
                daFunctions.ConnectionString.Close();

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
        public void InsertExpenseItem(int expenseId, DateTime expenseDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName)
        {
            //SqlConnection conn = new SqlConnection(connectionString);
            string query = String.Format("INSERT INTO ExpenseItem (ExpenseHeaderId, ExpenseDate, Location, Description, Amount, Currency,AudAmount,ReceiptFileName) VALUES({0},'{1}','{2}','{3}',{4},'{5}',{6},'{7}')", expenseId, expenseDate, location, description, amount, currency, audAmount, receiptFileName);
            SqlCommand cmd = new SqlCommand(query, daFunctions.ConnectionString);

            try
            {
                daFunctions.ConnectionString.Open();
                cmd.ExecuteNonQuery();
                daFunctions.ConnectionString.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting expense item: " + ex.Message);
            }
            
        }



        public void SupervisorUpdateReport()
        {
        }

        public void AccountsUpdateReport()
        {

        }

    }
}
