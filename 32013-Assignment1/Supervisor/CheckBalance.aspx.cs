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

namespace ThreeAmigos.ExpenseManagement.UserInterface.Supervisor
{
    public partial class CheckBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Employee emp = new Employee();
           emp =(Employee) Session["emp"];
                    
           BudgetTracker budget = new BudgetTracker();
           budget.DepartmentBudget(emp.Dept.MonthlyBudget, emp.Dept.DepartmentId);            
           decimal moneyRemaining = budget.RemainingAmount;
           decimal totalExpenseAmount = budget.TotalExpenseAmount;
           lblMoneySpent.Text = "Total money spent so far is : AU$ " + totalExpenseAmount.ToString();
           lblMoneyRemaining.Text = "Total money remaining is : AU$ " + moneyRemaining.ToString();
          
        }
    }
}