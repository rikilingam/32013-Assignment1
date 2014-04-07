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
        public int DepartmentId { get; set; }
        public DateTime CreateDate { get; set; }
        //public DateTime LastSaveDate { get; set; }
        //public DateTime SubmitDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ProcessedById { get; set; }
        public ReportStatus Status { get; set; }
        public List<ExpenseItem> expenseItems;

        public ExpenseReport()
        {
            ExpenseId = -1;
            expenseItems = new List<ExpenseItem>();
        }

        //ExpenseReportDAL expReport = new ExpenseReportDAL();
        
        //public void AddExpenseReport(int createdById, DateTime createDate, DateTime submitDate)
        //{
        //    expReport.AddExpenseReport(createdById, createDate,submitDate,ReportStatus.Submitted.ToString());
        //}

        //public int FetchExpenseId()
        //{
        //    return expReport.FetchExpenseId();
        //}
        

        /// <summary>
        /// Adds expense items to the report
        /// </summary>
        /// <param name="item">expense item</param>
        public void AddExpenseItem(ExpenseItem item)
        {
            expenseItems.Add(item);
        }

        /// <summary>
        /// Submits the initial expense report by the consultant
        /// </summary>
        public void SubmitExpenseReport()
        {            
            Status = ReportStatus.Submitted;
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            ExpenseId = expenseReportDAL.InsertExpenseHeader(CreatedById, CreateDate, DepartmentId, Status.ToString());

            //foreach (ExpenseItem item in expenseItems)
            //{
            //    item.SubmitExpenseItem(ExpenseId);
            //}

        }
                   

        public void SupervisorUpdateReport()
        {
        }

        public void AccountsUpdateReport()
        {
        
        }
    }
}
