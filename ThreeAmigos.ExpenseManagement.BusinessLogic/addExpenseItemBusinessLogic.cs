﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    class addExpenseItemBusinessLogic
    {
        public void addExpenseItem(ExpenseItem expItem)
        {
            addExpenseItemBusinessLogic item = new addExpenseItemBusinessLogic();
            item.addExpenseItem(expItem);
        }
    }
}
