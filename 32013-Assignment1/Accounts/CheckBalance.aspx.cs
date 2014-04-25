using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Accounts
{
	public partial class CheckBalance : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{         
            BudgetTracker budget = new BudgetTracker();
            budget.CompanyBudget();             
            decimal moneyRemaining = budget.RemainingAmount;
            decimal totalExpenseAmount = budget.TotalExpenseAmount;
            lblMoneySpent.Text =     "Total money spent so far is: AU$ " + totalExpenseAmount.ToString();
            lblMoneyRemaining.Text = "Company budget remaining is: AU$ " + moneyRemaining.ToString();

		}
	}
}