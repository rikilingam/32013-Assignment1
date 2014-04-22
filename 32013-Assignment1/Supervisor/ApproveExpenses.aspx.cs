using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Windows.Forms;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ApproveExpenses : System.Web.UI.Page
    {
        ExpenseReportBuilder expReportBuilder = new ExpenseReportBuilder();
        Employee emp = new Employee();
        List<ExpenseReport> expenseReport = new List<ExpenseReport>();

        protected void Page_Load(object sender, EventArgs e)
        {
            emp = (Employee)Session["emp"];
            expenseReport = expReportBuilder.GetReportsBySupervisor(emp.Dept.DepartmentId, ReportStatus.Submitted.ToString());
            if (!IsPostBack)
            {
                rptExpenseReport.DataSource = expenseReport;
                rptExpenseReport.DataBind();
            }
        }

        protected void rptExpenseReport_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BudgetTracker budget = new BudgetTracker();
            double totalSpent = budget.SumOfExpenseApproved(emp.Dept.DepartmentId);
            double moneyRemaining = budget.CalculateRemainingBudget(Convert.ToDouble(ConfigurationManager.AppSettings["DepartmentMonthlyBudget"]), totalSpent);

            if (e.CommandName == "RejectExpense")
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("btnReject");
                int expenseId = Convert.ToInt32(btn.CommandArgument);
                expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());
                repBind();
            }
            else if (e.CommandName == "ApproveExpense")
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("btnApprove");
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(',');
                int expenseId = Convert.ToInt32(arg[0]);
                double total = Convert.ToDouble(arg[1]);

                if (total > moneyRemaining)
                {
                    DialogResult UserReply = MessageBox.Show("Approving this expense will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
                    if (UserReply.ToString() == "Yes")
                    {
                        expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());
                        repBind();
                    }
                    else
                    {
                        expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());
                        repBind();
                    }
                }
                else
                {
                    expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());
                    repBind();
                }
            }
        }

        protected void repBind()
        {
            expenseReport = expReportBuilder.GetReportsBySupervisor(emp.Dept.DepartmentId, ReportStatus.Submitted.ToString());
            rptExpenseReport.DataSource = expenseReport;
            rptExpenseReport.DataBind();
        }

        protected void btnReceipt_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);

            string receiptFileName = btn.CommandArgument.ToString();

            string path = ConfigurationManager.AppSettings["ReceiptItemFilePath"];

            ClientScript.RegisterStartupScript(this.GetType(), "OpenReceipt", "OpenReceipt('" + path + receiptFileName + "');", true);
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


    }

}
