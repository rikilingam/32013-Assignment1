using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
    public enum ReportStatus { Submitted, RejectedBySupervisor, ApproveBySupervisor, RejectedByAccountant, ApprovedByAccountant }

    public class ExpenseReport
    {
        public int ExpenseId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ProcessedById { get; set; }
        public ReportStatus Status { get; set; }
        public List<ExpenseItem> ExpenseItems {get;set;}

        public ExpenseReport()
        {
            ExpenseItems = new List<ExpenseItem>();
        }


    }
}
