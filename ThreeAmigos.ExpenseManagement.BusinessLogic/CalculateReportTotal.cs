using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.BusinessObject;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class CalculateReportTotal
    {
        public static decimal ReportTotal(List<ExpenseItem> expenseItems)
        {
            decimal reportTotal=0;
            if (expenseItems!= null && expenseItems.Count > 0)
            {
                foreach (ExpenseItem item in expenseItems)
                {
                    reportTotal = reportTotal+ item.AudAmount;
                }
            }

            return reportTotal;
        }
    }
}
