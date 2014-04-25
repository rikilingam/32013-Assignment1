﻿using System;
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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ApproveExpenses : System.Web.UI.Page
    {
        ExpenseReportBuilder expReportBuilder=  new ExpenseReportBuilder();
        Employee emp = new Employee();
        BudgetTracker budget = new BudgetTracker();
       

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
           budget.DepartmentBudget(emp.Dept.MonthlyBudget, emp.Dept.DepartmentId);
           Session["budget"] = budget;
           UpdateBudgetMessage();
           rptExpenseReport.DataSource = expReportBuilder.GetReportsBySupervisor(emp.Dept.DepartmentId, ReportStatus.Submitted.ToString());
           rptExpenseReport.DataBind();
        }

        private void UpdateBudgetMessage()
        {
            Label1.Text = string.Format("You currently have <b>{0}</b> remaining from your departments monthly budget of <b>{1}</b>.",String.Format("{0:c}",budget.RemainingAmount),String.Format("{0:c}",budget.BudgetAmount));
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

        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string[] arg = new string[2];
            arg = btn.CommandArgument.ToString().Split(',');
            int expenseId = Convert.ToInt32(arg[0]);
            decimal expenseTotal = Convert.ToDecimal(arg[1]);

            budget = (BudgetTracker)Session["budget"];


            if ((budget.RemainingAmount - expenseTotal) < 0)
            {
                DialogResult UserReply = MessageBox.Show("Approving this expense " + expenseTotal + " will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
                if (UserReply.ToString() == "Yes")
                {
                    expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());

                }
                else
                {
                    expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());
                }
            }
            else
            {
                expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());

            }

            InitializeRepeater();
        }

        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            int expenseId = Convert.ToInt32(btn.CommandArgument);
            expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());

            InitializeRepeater();
        }


    }

}
