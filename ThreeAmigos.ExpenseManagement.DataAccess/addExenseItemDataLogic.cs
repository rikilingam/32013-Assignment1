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
   public class addExenseItemDataLogic
    {
        public void addExpenseItem(ExpenseItem expItem)
        {
            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into ExpenseItem values(@expenseDate,@location,@description,@amount,@currency,@aud,@filename,@employeeId,@expenseHeaderId)";

            cmd.Parameters.Add("@expenseDate", SqlDbType.Date, 50).Value = expItem.expenseDate;
            cmd.Parameters.Add("@location", SqlDbType.VarChar, 50).Value = expItem.location;
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 50).Value = expItem.description;
            cmd.Parameters.Add("@amount", SqlDbType.Float).Value = expItem.amount;
            cmd.Parameters.Add("@currency", SqlDbType.VarChar, 5).Value = expItem.currency;
            cmd.Parameters.Add("@aud", SqlDbType.Float).Value = expItem.audAmount;
            cmd.Parameters.Add("@filename", SqlDbType.VarChar, 50).Value = expItem.receiptFileName;
            cmd.Parameters.Add("@employeeId", SqlDbType.Int).Value = System.Web.HttpContext.Current.Session["userid"]; // Will work to fetch the data directly instead of sessions
            cmd.Parameters.Add("@expenseHeaderId", SqlDbType.Int).Value = expItem.ExpenseHeaderId;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }
    }
}
