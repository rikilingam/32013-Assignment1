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
        private SqlConnection connection;
        private SqlCommand command;
        
        public DataAccessFunctions()
        {            
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public SqlConnection Connection
        {
            get
            {     
                return connection;
            }
        }

        public SqlCommand Command
        {
            get { return command; }
            set { command = value;  }
        }
    }
}
