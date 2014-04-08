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
        protected string connString = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
    }
}
