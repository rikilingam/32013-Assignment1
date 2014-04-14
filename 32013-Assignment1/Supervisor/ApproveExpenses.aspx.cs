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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ApproveExpenses : System.Web.UI.Page
    {
        double currentReportSum = 0;
        double totalSpent = 0;

     //   ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {                
                Employee emp = new Employee();
               // Department dept = new Department();
                EmployeeDAL employeeDAL = new EmployeeDAL();
                ExpenseReportBuilder expReport = new ExpenseReportBuilder();

                emp = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);
                Session["EmpUserId"] = emp.UserId;
                Session["EmpDepartment"] = emp.Dept.DepartmentId;               
                Session["ExpenseReport"] = expReport.GetReportSummaryBySupervisor((int)Session["EmpDepartment"]);

                totalSpent = expReport.SumOfExpenseApproved((int)(Session["EmpDepartment"]));
                Response.Write("Total Amount spent is " + totalSpent);
                Session["remainingBudget"] =  expReport.CalculateRemainingBudget(Convert.ToDouble(ConfigurationManager.AppSettings["DepartmentMonthlyBudget"]),totalSpent) ;
                Response.Write("\nRemaining budget is "+ Session["remainingbudget"]);
                
                grdExpenseReport.DataSource = Session["ExpenseReport"];
                grdExpenseReport.DataBind();
            }              
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExpenseReportBuilder expReportBuilder = new ExpenseReportBuilder();
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            
            // find child gridview control to show items in expense report
            GridView grvExpenseItems = (GridView)grdExpenseReport.Rows[rowindex].FindControl("grdExpenseItems");

            grdExpenseReport.Rows[rowindex].FindControl("btnCancelItems").Visible = false;
            Session["ExpenseId"] = grdExpenseReport.Rows[rowindex].Cells[3].Text;
          
            if (e.CommandName == "Details")
            {
                grdExpenseReport.Rows[rowindex].FindControl("btnCancelItems").Visible = true;
                grdExpenseReport.Rows[rowindex].FindControl("btnItemDetails").Visible = false;

              //  ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                List<ExpenseReport> expReport = new List<ExpenseReport>();                
                expReport =(List<ExpenseReport>) Session["ExpenseReport"]; 
                for(int i=0;i<expReport.Count;i++)
                {
                    if (expReport[i].ExpenseId == Convert.ToInt32(grdExpenseReport.Rows[rowindex].Cells[3].Text))
                    {
                        grvExpenseItems.DataSource = expReport[i].ExpenseItems;
                        grvExpenseItems.DataBind();
                        grvExpenseItems.Visible = true;
                    break;
                    }                
                }
            }

            else if (e.CommandName == "ApproveExpense")
            {   
                List<ExpenseReport> expReport = new List<ExpenseReport>();
                expReport =(List<ExpenseReport>) Session["ExpenseReport"];

                // Use to check the sum of all items in the expense report
                for (int i = 0; i < expReport.Count; i++)
                {
                    if (expReport[i].ExpenseId == Convert.ToInt32(grdExpenseReport.Rows[rowindex].Cells[3].Text))
                    {
                      Session["ExpenseId"] = expReport[i].ExpenseId;
                      for (int a = 0; a < expReport[i].ExpenseItems.Count; a++)
                      {
                          currentReportSum = currentReportSum + expReport[i].ExpenseItems[a].Amount;
                      }
                    }                   
                 }
                
                
                if(currentReportSum>Convert.ToDouble(Session["remainingBudget"]))
                {
                    DialogResult UserReply = MessageBox.Show("Approving this expense will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
                    if (UserReply.ToString() == "Yes")
                    {
                        expReportBuilder.SupervisorAddExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid));
                    }
                    else
                    {
                        expReportBuilder.SupervisorRejectExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid));
                    }
                }
                
                else
                 {
                     expReportBuilder.SupervisorAddExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid));
                 }                 
            }

            else if (e.CommandName == "RejectExpense")
            {
               expReportBuilder.SupervisorRejectExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid));
            }

            else
            {
                // child gridview  display false when cancel button raise event
                grvExpenseItems.Visible = false;
                grdExpenseReport.Rows[rowindex].FindControl("btnItemDetails").Visible = true;
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           
        }
    }
}