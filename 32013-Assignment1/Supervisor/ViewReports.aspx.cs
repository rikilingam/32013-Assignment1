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
        Employee emp = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSearchExpenses_Click(object sender, EventArgs e)
        {
            emp = (Employee)Session["emp"]; 
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            rptExpenseReport.DataSource = expenseReportDAL.GetReportsByDepartment(emp.Dept.DepartmentId, ddlSearchFilter.SelectedValue);
            rptExpenseReport.DataBind();
        }

       protected void rptExpenseReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }


        


    }
}