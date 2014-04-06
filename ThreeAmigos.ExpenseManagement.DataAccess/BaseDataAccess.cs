using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString);
        protected SqlCommand cmd;
        protected SqlDataReader rdr;
    }
}
