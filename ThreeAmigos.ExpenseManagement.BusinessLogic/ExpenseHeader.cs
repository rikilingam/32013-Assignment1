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
        public int ExpenseId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastSaveDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public int CreatedById { get; set; }
        public int ApprovedById { get; set; }
        public int ProcessedById { get; set; }
        public ReportStatus Status { get; set; }

        DataAccess.ExpenseHeader expheader = new DataAccess.ExpenseHeader();
        
        //public void addExpenseHeader(BusinessObject.ExpenseHeader header)
        public void addExpenseHeader(ExpenseHeader header)
        {
            expheader.AddExpenseHeader(header);
        }

        public int FetchExpenseId()
        {
            return expheader.FetchExpenseId();
        }
    }
}
