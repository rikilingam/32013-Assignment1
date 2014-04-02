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
    public class addExpenseHeaderDataLogic
    {
        public void addExpenseHeader(ExpenseHeader header)
        {

            string connection = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into ExpenseHeader(CreatedById,CreateDate,SubmitDate) values(@CreatedById,@CreateDate,@SubmitDate)";

            cmd.Parameters.Add("@CreatedById", SqlDbType.Int).Value = header.CreatedById;
            cmd.Parameters.Add("@CreateDate", SqlDbType.Date).Value = header.CreateDate;
            cmd.Parameters.Add("@SubmitDate", SqlDbType.Date).Value = header.SubmitDate;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }
    }
}
