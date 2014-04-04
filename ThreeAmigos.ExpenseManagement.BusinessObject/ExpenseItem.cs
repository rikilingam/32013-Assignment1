using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
   public class ExpenseItem
    {
        public int itemId { get; set; }
        public DateTime expenseDate { get; set; }
        public string location{get;set;}
        public string description{get;set;}
        public double amount { get; set; }
        public string currency { get; set; }
        public double audAmount { get; set; }
        public string receiptFileName { get; set; }
        public int expenseHeaderId { get; set; }
    }
}
