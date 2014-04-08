using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
    public class ExpenseItem
    {
        public int ExpenseHeaderId { get; set; }
        public int ItemId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public double AudAmount { get; set; }
        public string ReceiptFileName { get; set; }
    }
}
