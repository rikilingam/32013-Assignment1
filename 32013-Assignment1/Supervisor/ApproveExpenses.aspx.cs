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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class ApproveExpenses : System.Web.UI.Page
    {
        ExpenseReportBuilder expReportBuilder=  new ExpenseReportBuilder();
        Employee emp = new Employee();
        BudgetTracker budget = new BudgetTracker();
                 
        //List<ExpenseReport> expenseReport = new List<ExpenseReport>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeRepeater();
            }
        }


        protected void InitializeRepeater()
        {

            if (Session["emp"] != null)
            {
                emp = (Employee)Session["emp"];
            }
            else
            {
                EmployeeDAL employeeDAL = new EmployeeDAL();
                emp = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);
                Session["emp"] = emp;
            }

            budget.DepartmentBudget(emp.Dept.MonthlyBudget, emp.Dept.DepartmentId);

            Label1.Text = "dept budget: " + emp.Dept.MonthlyBudget.ToString() + " budget: " + budget.RemainingAmount();
            rptExpenseReport.DataSource = expReportBuilder.GetReportsBySupervisor(emp.Dept.DepartmentId, ReportStatus.Submitted.ToString());
            rptExpenseReport.DataBind();
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
            decimal total = Convert.ToDecimal(arg[1]);

            if (total > budget.RemainingAmount())
            {
                DialogResult UserReply = MessageBox.Show("Approving this expense will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
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
