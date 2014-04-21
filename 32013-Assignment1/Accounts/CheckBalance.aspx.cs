﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.UserInterface.Accounts
{
    public partial class CheckBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMoneySpent.Visible = false;
            lblMoneyRemaining.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlFilter.SelectedValue == "Money Spent")
            {
                lblMoneySpent.Visible = true;
                lblMoneySpent.Text = "Total money spent so far is : AU$ " + Session["totalSpent"].ToString();
            }
            else
            {
                lblMoneyRemaining.Visible = true;
                lblMoneyRemaining.Text = "Total money remaining is : AU$ " + Session["remainingBudget"].ToString();
            }
        }
    }
}