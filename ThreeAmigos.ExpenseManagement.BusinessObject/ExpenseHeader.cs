using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeAmigos.ExpenseManagement.BusinessObject
{
    enum ReportStatus {Created, Submitted, RejectedBySupervisor, ApproveBySupervisor, RejectedByAccountant, ApprovedByAccountant}
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
        public int ProcessesById { get; set; }
        public ReportStatus Status { get; set; }

    }
}
