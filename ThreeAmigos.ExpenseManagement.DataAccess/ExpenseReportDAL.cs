using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class ExpenseReportDAL
    {
        public void AddExpenseReport(int createdById,DateTime createDate,DateTime submitDate,string status)
        {
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "AddExpenseHeader";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@createdById", createdById);
            cmd.Parameters.AddWithValue("@createDate", createDate);
            cmd.Parameters.AddWithValue("@submitDate", submitDate);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }

        public int FetchExpenseId()
        {
            int expenseId;
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select ExpenseId from ExpenseHeader where ExpenseId=Ident_Current('ExpenseHeader')";
            expenseId = Convert.ToInt32(cmd.ExecuteScalar());
            return expenseId;
        }
    }
}
