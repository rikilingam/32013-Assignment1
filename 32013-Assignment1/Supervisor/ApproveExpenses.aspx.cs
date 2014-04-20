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
        ExpenseReportDAL eexp = new ExpenseReportDAL(); 
        double currentReportSum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
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
            Session["ExpenseId"] = grdExpenseReport.Rows[rowindex].Cells[4].Text;
          
            if (e.CommandName == "Details")
            {
                grdExpenseReport.Rows[rowindex].FindControl("btnCancelItems").Visible = true;
                grdExpenseReport.Rows[rowindex].FindControl("btnItemDetails").Visible = false;

              //  ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                List<ExpenseReport> expReport = new List<ExpenseReport>();                
                expReport =(List<ExpenseReport>) Session["ExpenseReport"]; 
                for(int i=0;i<expReport.Count;i++)
                {
                    if (expReport[i].ExpenseId == Convert.ToInt32(grdExpenseReport.Rows[rowindex].Cells[4].Text))
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

               
                for (int i = 0; i < expReport.Count; i++)
                {
                    if (expReport[i].ExpenseId == Convert.ToInt32(grdExpenseReport.Rows[rowindex].Cells[4].Text))
                    {
                      Session["ExpenseId"] = expReport[i].ExpenseId;

                      // Use to check the sum of all items in the expense report
                      for (int a = 0; a < expReport[i].ExpenseItems.Count; a++)
                      {
                        currentReportSum= CalculateReportTotal.ReportTotal(expReport[i].ExpenseItems);
                      }
                    }   
                
                 }

                if (currentReportSum > Convert.ToDouble(Session["remainingBudget"]))
                {
                    DialogResult UserReply = MessageBox.Show("Approving this expense will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
                    if (UserReply.ToString() == "Yes")
                    {
                        expReportBuilder.SupervisorActionOnExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid), ReportStatus.ApprovedBySupervisor.ToString());
                        gridBind();                 
                    }
                    else
                    {
                        expReportBuilder.SupervisorActionOnExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid), ReportStatus.RejectedBySupervisor.ToString());
                        gridBind();  
                    }
                }

                else
                {
                    expReportBuilder.SupervisorActionOnExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid), ReportStatus.ApprovedBySupervisor.ToString());
                    gridBind();  
                }          
            }

            else if (e.CommandName == "RejectExpense")
            {
                expReportBuilder.SupervisorActionOnExpenseReport(Convert.ToInt32(Session["ExpenseId"]), Session["EmpUserId"] as Guid? ?? default(Guid), ReportStatus.RejectedBySupervisor.ToString());
                gridBind();
            }

            else if (e.CommandName == "OpenReceipt")
            {
                int expenseId = Convert.ToInt32(grdExpenseReport.Rows[rowindex].Cells[4].Text);
                string fileName = expReportBuilder.GetFileName(expenseId); 
                string filePath=Server.MapPath("~/Receipts/"+fileName);
                Response.ContentType = "application/pdf";
                Response.WriteFile(filePath);
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

        protected void gridBind()
        {
            Session["ExpenseReport"] = eexp.GetReportsBySupervisor((int)Session["EmpDepartment"], ReportStatus.Submitted.ToString());
            grdExpenseReport.DataSource = Session["ExpenseReport"];
            grdExpenseReport.DataBind();
        }
    }
}