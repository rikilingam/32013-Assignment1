using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// Checks the role of the user after authenication and redirects to appropriate page
        /// </summary>
        protected void loginMain_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (User.IsInRole("Consultant"))
            {
                Response.Redirect("/Consultant/Default.aspx");
            }
            else if (User.IsInRole("Supervisor"))
            {
                Response.Redirect("/Supervisor/Default.aspx");
            }
            else if (User.IsInRole("Accounts"))
            {
                Response.Redirect("/Accounts/Default.aspx");
            }
            //else
            //{
            //    Response.Redirect("Default.aspx");
            //}
        }
    }
}