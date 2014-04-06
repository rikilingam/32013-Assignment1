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
    public class ExpenseReportDAL:BaseDataAccess
    {

        // should pass the expense report object.
        public void AddExpenseReport(int createdById,DateTime createDate,DateTime submitDate,string status)
        {
           // string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            //SqlConnection con = new SqlConnection(connection);

            // add try connection
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "AddExpenseHeader";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@createdById", createdById);
            cmd.Parameters.AddWithValue("@createDate", createDate);
            cmd.Parameters.AddWithValue("@submitDate", submitDate);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // insert expense header return expense id
         


        // insert expense items need param expense id.

        public int FetchExpenseId()
        {
            int expenseId;
            //string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            //SqlConnection con = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select ExpenseId from ExpenseHeader where ExpenseId=Ident_Current('ExpenseHeader')";
            expenseId = Convert.ToInt32(cmd.ExecuteScalar());
            return expenseId;
        }
    }
}
