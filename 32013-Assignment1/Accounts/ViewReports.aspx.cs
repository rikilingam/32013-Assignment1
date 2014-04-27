using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Accounts
{
    public partial class ViewReports : System.Web.UI.Page
    {
        Employee emp = new Employee();
        BudgetTracker comBudget = new BudgetTracker();  // company budget
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal usedAmount = 0;
            decimal overAmount = 0;
            comBudget.CompanyBudget();
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
                    string.Format("<color=red><b>{0}</b></color> have been approved, which is <b>{1} over</b> the company monthly budget of <b>{2}</b>.",
                    String.Format("{0:c}", usedAmount),
                    String.Format("{0:c}", overAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
        }

        protected void btnSearchExpenses_Click(object sender, EventArgs e)
        {
            emp = (Employee)Session["emp"];
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            rptExpenseReport.DataSource = expenseReportDAL.GetReportsByAllDepartment(ddlSearchFilter.SelectedValue);
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