using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.DataAccess
{
    public class DataAccessFunctions
    {
        private SqlConnection connectionString;
        
        public DataAccessFunctions()
        {            
            connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString);
        }

        public SqlConnection ConnectionString
        {
            get
            {                
                return connectionString;
            }
        }
    }
}
