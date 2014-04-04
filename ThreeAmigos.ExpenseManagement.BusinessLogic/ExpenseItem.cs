using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    class ExpenseItem
    {
        public void AddExpenseItem(BusinessObject.ExpenseItem expItem)
        {
           DataAccess.ExpenseItem item = new DataAccess.ExpenseItem();
           item.AddExpenseItem(expItem);
        }
    }
}
