using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class addExpenseHeaderBusinessLogic
    {
        public void addExpenseHeader(ExpenseHeader header)
        {
            addExpenseHeaderDataLogic expheader = new addExpenseHeaderDataLogic();
            expheader.addExpenseHeader(header);
        }
    }
}
