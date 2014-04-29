using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using System.Windows.Forms;
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
        ExpenseReportBuilder expReportBuilder = new ExpenseReportBuilder();
        ExpenseReportDAL expDAL = new ExpenseReportDAL();
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
            if (Session["emp"] != null)
            {
                budget.DepartmentBudget(emp.Dept.MonthlyBudget, emp.Dept.DepartmentId);
                Session["budget"] = budget;
            }
            else
            {
                EmployeeDAL employeeDAL = new EmployeeDAL();
                emp = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);
                budget.DepartmentBudget(emp.Dept.MonthlyBudget, emp.Dept.DepartmentId);
                Session["budget"] = budget;
            }

            UpdateBudgetMessage();
            rptExpenseReport.DataSource = expDAL.GetReportsByDepartment(emp.Dept.DepartmentId, ReportStatus.Submitted.ToString());
            rptExpenseReport.DataBind();
        }

        private void UpdateBudgetMessage()
        {
            lblBudgetMessage.Text = string.Format("You currently have <b>{0}</b> remaining from your departments monthly budget of <b>{1}</b>.", String.Format("{0:c}", budget.RemainingAmount), String.Format("{0:c}", budget.BudgetAmount));
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


        //Approve expense is under budget or show warning if over budget
        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string[] arg = new string[2];
            arg = btn.CommandArgument.ToString().Split(',');
            int expenseId = Convert.ToInt32(arg[0]);
            decimal expenseTotal = Convert.ToDecimal(arg[1]);

            if (Session["budget"] == null)
            {
                InitializeRepeater();
                budget = (BudgetTracker)Session["budget"];
            }
            else
            {
                budget = (BudgetTracker)Session["budget"];
            }


            if ((budget.RemainingAmount - expenseTotal) < 0)
            {
                //DialogResult UserReply = MessageBox.Show("Approving this expense " + expenseTotal + " will cross the total monthly budget...You want to approve?", "Important Question", MessageBoxButtons.YesNo);
                lblBudgetWarning.Text = "Approving this expense for " + String.Format("{0:c}", expenseTotal) + " will result in the monthly department budget being exceeded, do you want to approve?";
                hdnExpenseId.Value = expenseId.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "BudgetWarningModal", "ShowBudgetWarningModal();", true);

                //if (UserReply.ToString() == "Yes")
                //{
                //    expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());

                //}
                //else
                //{
                //    expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());
                //}
            }
            else
            {
                expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());

            }

            InitializeRepeater();
        }

        //Reject expense when budget is below max budget
        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            int expenseId = Convert.ToInt32(btn.CommandArgument);
            expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());

            InitializeRepeater();
        }

        //Approved expense when budget will be exceeded
        protected void btnConfirmApprove_Click(object sender, EventArgs e)
        {            
            int expenseId=-1;
            if (IsExpenseIdValid(hdnExpenseId.Value, out expenseId))
            {
                expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.ApprovedBySupervisor.ToString());
            }

            InitializeRepeater();
        }

        //Reject expense when budget will be exeeded
        protected void btnConfirmReject_Click(object sender, EventArgs e)
        {
            int expenseId = -1;
            if (IsExpenseIdValid(hdnExpenseId.Value, out expenseId))
            {
                expReportBuilder.SupervisorActionOnExpenseReport(expenseId, emp.UserId, ReportStatus.RejectedBySupervisor.ToString());
            }
            
            InitializeRepeater();
        }

        //check that the expense id is valid
        protected bool IsExpenseIdValid(string id, out int expenseId)
        {

            if (Int32.TryParse(id, out expenseId) && expenseId > 0)
                return true;
            else
                return false;
        }

    }

}
