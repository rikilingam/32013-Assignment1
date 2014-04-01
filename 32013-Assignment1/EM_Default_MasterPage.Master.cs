using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public partial class EM_NoMembership_MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // disable all navigation items on page load
            navConsultant.Visible = false;
            navSupervisor.Visible = false;
            navAccounts.Visible = false;

            // create an array which stores a list of roles the current user is a member of
            string[] myRoles;
            myRoles = Roles.GetRolesForUser();

            //enable navigation items for each role the current users is a member of
            if (myRoles.Length > 0)
            {
                foreach (string role in myRoles)
                {
                    if (role == "Consultant")
                    {
                        navConsultant.Visible = true;

                    }
                    else if (role == "Supervisor")
                    {
                        navSupervisor.Visible = true;
                    }
                    else if (role == "Accounts")
                    {
                        navAccounts.Visible = true;
                    }
                }
            }
            else
            {
                maincontent.InnerText = "User " + HttpContext.Current.User.Identity.Name  + " is not a member of any roles!";
            }
        }
    }
}