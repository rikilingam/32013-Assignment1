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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Accounts
{
	public partial class CheckExpenseApproved : System.Web.UI.Page
	{
        ExpenseReportBuilder expReportBuilder = new ExpenseReportBuilder();
        Employee emp = new Employee();
        BudgetTracker comBudget = new BudgetTracker();  // company budget
        BudgetTracker deptBudget = new BudgetTracker(); // department budget

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
            comBudget.CompanyBudget();
            Session["comBudget"] = comBudget;
            UpdateBudgetMessage();

            rptExpenseReport.DataSource =
                expReportBuilder.GetExpenseReportsBySupervisor();
            rptExpenseReport.DataBind();
        }

        private void UpdateBudgetMessage()
        {
            if (comBudget.RemainingAmount >= 0)
            {
                lblBudgetMessage.Text =
                    string.Format("You currently have <b>{0}</b> remaining from the company monthly budget of <b>{1}</b>.",
                    String.Format("{0:c}", comBudget.RemainingAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
            else
            {
                decimal overAmount = 0 - comBudget.RemainingAmount;
                decimal usedAmount = comBudget.BudgetAmount + overAmount;
                lblBudgetMessage.Text =
                    string.Format("You have approved <b>{0}</b>, which is <b>{1}</b> over the company monthly budget of <b>{2}</b>.",
                    String.Format("{0:c}", usedAmount),
                    String.Format("{0:c}", overAmount),
                    String.Format("{0:c}", comBudget.BudgetAmount));
            }
        }
	}
}