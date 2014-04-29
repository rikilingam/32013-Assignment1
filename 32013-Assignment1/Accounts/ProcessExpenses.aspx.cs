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
        ExpenseReportDAL expDAL = new ExpenseReportDAL();
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
                expDAL.GetReportsByAllDepartment(ReportStatus.ApprovedBySupervisor.ToString());
            rptExpenseReport.DataBind();
        }

        private void UpdateBudgetMessage()
        {
            decimal usedAmount = 0;
            decimal overAmount = 0;
            if (comBudget.RemainingAmount >= 0)
            {
                usedAmount = comBudget.BudgetAmount - comBudget.RemainingAmount;
                lblBudgetMessage.Text =
                    string.Format("<b>{0}</b> have been approved. You currently have <b>{1} remaining</b> for approval from the company monthly budget of <b>{2}</b>.",
                    String.Format("{0:c}", usedAmount),
                    String.Format("{0:c}", comBudget.RemainingAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
            else
            {
                overAmount = 0 - comBudget.RemainingAmount;
                usedAmount = comBudget.BudgetAmount + overAmount;
                lblBudgetMessage.Text =
                    string.Format("<b>{0}</b> have been approved, which is <b>{1} over</b> the company monthly budget of <b>{2}</b>.",
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

        //Approve expense if expense exceeded show warning
        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string[] arg = new string[2];
            arg = btn.CommandArgument.ToString().Split(',');
            int expenseId = Convert.ToInt32(arg[0]);
            decimal expenseTotal = Convert.ToDecimal(arg[1]);
            comBudget.CompanyBudget();

            //if (expenseTotal > comBudget.RemainingAmountAccounts)
            if (comBudget.IsBudgetExceeded(expenseTotal))
            {
                //DialogResult UserReply = MessageBox.Show("Approving this expense " + expenseTotal + " will cross the total monthly budget of the company. Do you want to approve?", "Important Question", MessageBoxButtons.YesNoCancel);

                //if (UserReply.ToString() == "Yes")
                //{
                //    expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedByAccounts.ToString());
                //}
                //else if (UserReply.ToString() == "No")
                //{
                //    expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedByAccounts.ToString());
                //}

                lblBudgetWarning.Text = "Approving this expense for " + String.Format("{0:c}", expenseTotal) + " will result in the monthly company budget being exceeded, do you want to approve?";
                hdnExpenseId.Value = expenseId.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "BudgetWarningModal", "ShowBudgetWarningModal();", true);

            }
            else
            {
                expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedByAccounts.ToString());
            }

            InitializeRepeater();
        }

        //Reject expense when budget has not been exceeded
        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            int expenseId = Convert.ToInt32(btn.CommandArgument);
            expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedByAccounts.ToString());

            InitializeRepeater();
        }

        //Approve expense when budget will be exceeded
        protected void btnConfirmApprove_Click(object sender, EventArgs e)
        {
            int expenseId = -1;
            if (IsExpenseIdValid(hdnExpenseId.Value, out expenseId))
            {
                expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedByAccounts.ToString());
                
            }

            InitializeRepeater();
        }

        //Reject expense when budget will be exeeded
        protected void btnConfirmReject_Click(object sender, EventArgs e)
        {
            int expenseId = -1;
            if (IsExpenseIdValid(hdnExpenseId.Value, out expenseId))
            {
                expReportBuilder.AccountantActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedByAccounts.ToString());
            }

            InitializeRepeater();
        }

        //check that the expense id is valid
        protected bool IsExpenseIdValid(string id, out int expenseId)
        {

            if (Int32.TryParse(id, out expenseId) && expenseId > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Highlight the footer of expense report if the amount will result in the department budget being exceeded
        /// </summary>
        /// <param name="amount">Total amount of the expense report</param>
        /// <param name="dept">Department expensed to</param>
        /// <returns>background styling</returns>
        protected string HighlightOverBudget(decimal amount, Department dept)
        {
            BudgetTracker deptBudget = new BudgetTracker();
            deptBudget.DepartmentBudget(dept.MonthlyBudget, dept.DepartmentId);

            if (deptBudget.IsBudgetExceededAccounts(amount))
                return "background-color:#f2dede; color:red";
            else
                return "background-color:#dff0d8";
        }

    }
}