using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class ExpenseReportDAL : BaseDataAccess
    {
        //public void AddExpenseReport(id createdById, DateTime createDate, DateTime submitDate, string status)
        //{
        //    // string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
        //    //SqlConnection con = new SqlConnection(connection);

        //    // add try connection
        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "AddExpenseHeader";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@createdById", createdById);
        //    cmd.Parameters.AddWithValue("@createDate", createDate);
        //    cmd.Parameters.AddWithValue("@submitDate", submitDate);
        //    cmd.Parameters.AddWithValue("@status", status);
        //    cmd.ExecuteNonQuery();
        //    cmd.Dispose();
        //    conn.Close();
        //}

        /// <summary>
        /// Inserts the expense header
        /// </summary>
        /// <returns>returns the expense id</returns>
        public int InsertExpenseHeader(Guid createdById, DateTime createDate, int departmentId, string status)
        {
            int expenseId = -1;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conn;
                cmd.CommandText = "AddExpenseHeader";
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter expenseId = new SqlParameter("@id", SqlDbType.Int);
                //expenseId.Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@CreatedById", createdById);
                cmd.Parameters.AddWithValue("@CreateDate", createDate);
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                expenseId = Convert.ToInt32(cmd.Parameters["@Id"].Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting the expense header: " + ex.Message);

            }

            return expenseId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="expenseDate"></param>
        /// <param name="location"></param>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="audAmount"></param>
        /// <param name="receiptFileName"></param>
        public void InsertExpenseItem(int expenseId, DateTime expenseDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName)
        {
            SqlConnection conn = new SqlConnection(connString);
            string query = String.Format("INSERT INTO ExpenseItem (ExpenseHeaderId, ExpenseDate, Location, Description, Amount, Currency,AudAmount,ReceiptFileName) VALUES({0},'{1}','{2}','{3}',{4},'{5}',{6},'{7}')", expenseId, expenseDate, location, description, amount, currency, audAmount, receiptFileName);
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem inserting expense item: " + ex.Message);
            }
            
        }



        //public int FetchExpenseId()
        //{
        //    int expenseId;
        //    //string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
        //    //SqlConnection con = new SqlConnection(connection);
        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "select ExpenseId from ExpenseHeader where ExpenseId=Ident_Current('ExpenseHeader')";
        //    expenseId = Convert.ToInt32(cmd.ExecuteScalar());
        //    return expenseId;
        //}
    }
}
