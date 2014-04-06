using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
   public class ExpenseItemDAL
    {
       public void AddExpenseItem(DateTime expDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName, int expHeaderItem)
       {
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "AddExpenseItem";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@expenseDate",expDate);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@currency", currency);
            cmd.Parameters.AddWithValue("@audAmount", audAmount);
            cmd.Parameters.AddWithValue("@receiptFileName", receiptFileName);
            cmd.Parameters.AddWithValue("@expenseHeaderId", expHeaderItem);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
