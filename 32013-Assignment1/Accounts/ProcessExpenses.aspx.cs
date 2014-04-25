using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using System.Windows.Forms;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Accounts
{
    public partial class ProcessExpenses : System.Web.UI.Page
    {
        ExpenseReportBuilder expReportBuilder = new ExpenseReportBuilder();
        Employee emp = new Employee();
        BudgetTracker comBudget = new BudgetTracker();  // company budget
        BudgetTracker deptBudget = new BudgetTracker(); // department budget

        protected void Page_Load(object sender, EventArgs e)
        {
            emp = (Employee)Session["emp"];
            if (!IsPostBack)
            {
                InitializeRepeater();
            }
        }
       
        protected void InitializeRepeater()
        {
            comBudget.CompanyBudget();
            Session["comBudget"] = comBudget;
            UpdateBudgetMessage();
            rptExpenseReport.DataSource = 
            expReportBuilder.GetReportsByAccountant(ReportStatus.ApprovedBySupervisor.ToString());
            rptExpenseReport.DataBind();
        }

        private void UpdateBudgetMessage()
        {
            if (comBudget.RemainingAmount >= 0) 
            {
                lblBudgetMessage.Text =
                    string.Format("You currently have <b>{0}</b> remaining from the company monthly budget of <b>{1}</b>.",
                    String.Format("{0:c}", comBudget.RemainingAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
            else
            {
                decimal overAmount = 0 - comBudget.RemainingAmount;
                decimal usedAmount = comBudget.BudgetAmount + overAmount;
                lblBudgetMessage.Text =
                    string.Format("You have approved <b>{0}</b>, which is <b>{1}</b> over the company monthly budget of <b>{2}</b>.",
                    String.Format("{0:c}", usedAmount),
                    String.Format("{0:c}", overAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
        }

        protected void rptExpenseItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // hides the receipt button if the expense item does not contain a receipt file name
                ImageButton btn = (ImageButton)e.Item.FindControl("btnReceipt");
                if (string.IsNullOrEmpty(btn.CommandArgument.ToString()))
                {
                    btn.Visible = false;
                }
            }
        }

        protected void btnReceipt_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string receiptFileName = btn.CommandArgument.ToString();
            string path = ConfigurationManager.AppSettings["ReceiptItemFilePath"];
            ClientScript.RegisterStartupScript(this.GetType(), "OpenReceipt", "OpenReceipt('" + path + receiptFileName + "');", true);
        }

        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string[] arg = new string[2];
            arg = btn.CommandArgument.ToString().Split(',');
            int expenseId = Convert.ToInt32(arg[0]);
            decimal expenseTotal = Convert.ToDecimal(arg[1]);

            //if (expenseTotal > comBudget.RemainingAmount)
            //{
                //DialogResult UserReply = MessageBox.Show("Approving this expense " + expenseTotal + " will cross the total monthly budget of the company. Do you want to approve?", "Important Question", MessageBoxButtons.YesNo);
                //if (UserReply.ToString() == "Yes")
                //{
            expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedByAccountant.ToString());
                //}
                //else
                //{
                    //expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedByAccountant.ToString());
            //    }
            //}
            //else
            //{
            //    expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedByAccountant.ToString());
            //}

            InitializeRepeater();
        }

        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            int expenseId = Convert.ToInt32(btn.CommandArgument);
            expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedByAccountant.ToString());

            InitializeRepeater();
        }

        /// <summary>
        /// Highlight expense report that will result in a department going over budget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        //protected void FormatRepeaterRow(Object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {
        //        // get department ID of the expense report
        //        Label lblDept = e.Item.FindControl("lblDepartmentId") as Label;
        //        int deptID = Convert.ToInt16(lblDept.Text);  
                  
        //        // get the total amount of expenses which were approved by accountant
        //        SpendTrackerDAL spendTrack = new SpendTrackerDAL();
        //        decimal deptBudgetProcessed = spendTrack.TotalExpenseAmountByDeptProcessed(deptID);
        //        //decimal deptBudgetProcessed = deptBudget.SumOfExpenseProcessed(deptID);
                
        //        Label lblExpense = e.Item.FindControl("lblExpense") as Label;
        //        decimal exp = Convert.ToDecimal (lblExpense.Text);  // get the amount of the expense report

        //        // get the monthly budget of the department
        //        decimal deptBudget = decimal.Parse(ConfigurationManager.AppSettings["DepartmentMonthlyBudget"]);

        //        // get the labels to highlight if over budget
        //        Label lblDate = e.Item.FindControl("lblDate") as Label; 
        //        Label lblConsultant = e.Item.FindControl("lblConsultant") as Label;
        //        Label lblSupervisor = e.Item.FindControl("lblSupervisor") as Label;
        //        Label lblDepartment = e.Item.FindControl("lblDepartment") as Label;
        //        Label lblStatus = e.Item.FindControl("lblStatus") as Label;
        //        Label lblExp = e.Item.FindControl("lblExp") as Label;

        //        // highlight if the expense of the report is more than the remaining budget of the department
        //        if (exp > deptBudget - deptBudgetProcessed)
        //        {
        //            lblDate.BackColor = System.Drawing.Color.Yellow;
        //            lblConsultant.BackColor = System.Drawing.Color.Yellow;
        //            lblSupervisor.BackColor = System.Drawing.Color.Yellow;
        //            lblDepartment.BackColor = System.Drawing.Color.Yellow;
        //            lblStatus.BackColor = System.Drawing.Color.Yellow;
        //            lblExp.BackColor = System.Drawing.Color.Yellow;
        //        }
        //    } 
        //}

        protected string HighlightOverBudget(decimal amount, Department dept)
        {
            BudgetTracker deptBudget = new BudgetTracker();
            deptBudget.DepartmentBudget(dept.MonthlyBudget,dept.DepartmentId);

            if (deptBudget.IsBudgetExceeded(amount))
                return "background-color:#f2dede; color:red";
            else
                return "background-color:#dff0d8";
        }

    }
}