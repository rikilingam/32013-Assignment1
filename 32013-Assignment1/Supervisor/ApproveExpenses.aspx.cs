using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ApproveExpenses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                Employee emp = new Employee();
                EmployeeDAL employeeDAL = new EmployeeDAL();
                
                ExpenseReportBuilder exp = new ExpenseReportBuilder();

                emp = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);

                Session["EmpDepartment"] = emp.DepartmentId;
                Session["ExpenseReport"] = expenseReportDAL.GetReportSummaryBySupervisor((int)Session["EmpDepartment"]);
                GridView1.DataSource = Session["ExpenseReport"];
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            //// find child gridview control
            GridView grv = (GridView)GridView1.Rows[rowindex].FindControl("GridView2");

            ////  text for which details display
          //  Label lbl = (Label)GridView1.Rows[rowindex].FindControl("Label2");
            GridView1.Rows[rowindex].FindControl("btnCancelItems").Visible = false;

          
            if (e.CommandName == "Details")
            {
                GridView1.Rows[rowindex].FindControl("btnCancelItems").Visible = true;
                GridView1.Rows[rowindex].FindControl("btnItemDetails").Visible = false;

                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                List<ExpenseReport> exp = new List<ExpenseReport>();                
                exp =(List<ExpenseReport>) Session["ExpenseReport"]; 
                for(int i=0;i<exp.Count;i++)
                {
                    if(exp[i].ExpenseId ==Convert.ToInt32(GridView1.Rows[rowindex].Cells[1].Text))
                    {
                    grv.DataSource = exp[i].ExpenseItems;
                    grv.DataBind();
                    grv.Visible = true;
                    break;
                    }                
                }
            }
            else
            {
                //// child gridview  display false when cancel button raise event
                grv.Visible = false;
                GridView1.Rows[rowindex].FindControl("btnItemDetails").Visible = true;
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           
        }
    }
}