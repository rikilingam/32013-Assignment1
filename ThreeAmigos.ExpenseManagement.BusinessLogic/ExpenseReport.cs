using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public enum ReportStatus { Submitted, RejectedBySupervisor, ApproveBySupervisor, RejectedByAccountant, ApprovedByAccountant }
   
    public class ExpenseReport
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
        public List<ExpenseItem> expenseItems;

        //ExpenseReportDAL expReport = new ExpenseReportDAL();
        
        //public void AddExpenseReport(int createdById, DateTime createDate, DateTime submitDate)
        //{
        //    expReport.AddExpenseReport(createdById, createDate,submitDate,ReportStatus.Submitted.ToString());
        //}

        //public int FetchExpenseId()
        //{
        //    return expReport.FetchExpenseId();
        //}
    }
}
