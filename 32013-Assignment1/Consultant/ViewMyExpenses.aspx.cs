using System;
using System.Collections.Generic;
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

            //gvDisplayExpenseReports.DataSource = expenseReportDAL.GetExpenseReportsByConsultant((Guid)Membership.GetUser().ProviderUserKey, ddlSearchFilter.SelectedValue);
            //gvDisplayExpenseReports.DataBind();
        }



        //protected void gvDisplayExpenseReports_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int expenseId = (int)gvDisplayExpenseReports.DataKeys[e.Row.RowIndex].Value;
        //        GridView gvExpenseItems = (GridView)e.Row.FindControl("gvExpenseItems");
        //        List<ExpenseItem> expenseItems = new List<ExpenseItem>();
        //        expenseItems = (string)e.Row.FindControl("expenseItems");
        //        gvExpenseItems.DataSource = (ObjectDataSource)e.Row.FindControl("gvExpenseItems") ;
        //        gvExpenseItems.DataBind();
        //    }
        //}


    }
}