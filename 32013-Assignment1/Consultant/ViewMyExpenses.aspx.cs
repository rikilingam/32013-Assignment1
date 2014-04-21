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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Consultant
{
    public partial class ViewMyExpenses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearchExpenses_Click(object sender, EventArgs e)
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();

            rptExpenseReport.DataSource = expenseReportDAL.GetExpenseReportsByConsultant((Guid)Membership.GetUser().ProviderUserKey, ddlSearchFilter.SelectedValue);
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