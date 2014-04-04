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
        DataAccess.ExpenseHeader expheader = new DataAccess.ExpenseHeader();
        
        public void addExpenseHeader(BusinessObject.ExpenseHeader header)
        {
            expheader.AddExpenseHeader(header);
        }

        public int FetchExpenseId()
        {
            return expheader.FetchExpenseId();
        }
    }
}
