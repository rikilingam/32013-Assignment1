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
 
namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ViewMyExpenses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearchExpenses_Click(object sender, EventArgs e)
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            rptExpenseReport.DataSource = expenseReportDAL.GetReportsBySupervisor(Convert.ToInt32(Session["empDepartment"]), ddlSearchFilter.SelectedValue);
            rptExpenseReport.DataBind();
        }

       protected void rptExpenseReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }


        


    }
}