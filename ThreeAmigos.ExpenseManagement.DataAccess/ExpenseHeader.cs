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
    public class ExpenseHeader
    {
        public void AddExpenseHeader(BusinessObject.ExpenseHeader header)
        {
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "AddExpenseHeader";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CreatedById",header.CreatedById);
            cmd.Parameters.AddWithValue("@CreateDate",header.CreateDate);
            cmd.Parameters.AddWithValue("@SubmitDate",header.SubmitDate);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public int FetchExpenseId()
        {
            int expenseId;
            string connection = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
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
