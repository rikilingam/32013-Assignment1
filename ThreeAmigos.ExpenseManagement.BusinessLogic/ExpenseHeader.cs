using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class ExpenseHeader
    {
        public void addExpenseHeader(BusinessObject.ExpenseHeader header)
        {
            DataAccess.ExpenseHeader expheader = new DataAccess.ExpenseHeader();
            expheader.AddExpenseHeader(header);
        }
    }
}
