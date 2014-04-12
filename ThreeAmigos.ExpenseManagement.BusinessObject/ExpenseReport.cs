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
        //public Guid CreatedById { get; set; }
        //public Guid ApprovedById { get; set; }
        //public Guid ProcessedById { get; set; }

        public Employee CreatedBy { get; set; }
        public Employee ApprovedBy { get; set; }
        public Employee ProcessedBy { get; set; }

        public ReportStatus Status { get; set; }
        public List<ExpenseItem> ExpenseItems {get;set;}

        public ExpenseReport()
        {
            CreatedBy = new Employee();
            ApprovedBy = new Employee();
            ProcessedBy = new Employee();
            ExpenseItems = new List<ExpenseItem>();
        }


    }
}
