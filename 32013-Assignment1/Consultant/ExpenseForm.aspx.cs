using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public partial class ExpenseForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtExpenseDate.Text = DateTime.Now.ToString();
        }

        protected void btnAddExpenseItem_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),"ExpenseItemModal","ShowExpenseItemModal();", true);
        }
    }
}