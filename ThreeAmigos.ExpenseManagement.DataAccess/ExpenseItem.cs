using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
   public class ExpenseItem
    {
        public void addExpenseItem(BusinessObject.ExpenseItem item)
        {
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "AddExpenseItem";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue ("@expenseDate", item.expenseDate);
            cmd.Parameters.AddWithValue ("@location",item.location);
            cmd.Parameters.AddWithValue ("@description", item.description);
            cmd.Parameters.AddWithValue ("@amount", item.amount);
            cmd.Parameters.AddWithValue ("@currency", item.currency);
            cmd.Parameters.AddWithValue ("@aud", item.audAmount);
            cmd.Parameters.AddWithValue ("@filename",item.receiptFileName);
            cmd.Parameters.AddWithValue ("@expenseHeaderId", item.expenseHeaderId);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }
    }
}
