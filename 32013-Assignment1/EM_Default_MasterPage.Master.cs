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
                        Employee emp = new Employee();
                        //Session["userId"] = emp.FetchUserId(HttpContext.Current.User.Identity.Name);
                        navConsultant.Visible = true;

                    }
                    else if (role == "Supervisor")
                    {
                        double totalSpent = 0;
                        navSupervisor.Visible = true;
                        Employee emp = new Employee();
                        EmployeeDAL employeeDAL = new EmployeeDAL();
                        ExpenseReportBuilder expReport = new ExpenseReportBuilder();

                        emp = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);
                        Session["EmpUserId"] = emp.UserId;
                        Session["EmpDepartment"] = emp.Dept.DepartmentId;
                        Session["ExpenseReport"] = expReport.GetReportsBySupervisor((int)Session["EmpDepartment"], ReportStatus.Submitted.ToString());

                        totalSpent = expReport.SumOfExpenseApproved((int)(Session["EmpDepartment"]));
                        Session["totalSpent"] = totalSpent;
                        Session["remainingBudget"] = expReport.CalculateRemainingBudget(Convert.ToDouble(ConfigurationManager.AppSettings["DepartmentMonthlyBudget"]), totalSpent);  
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

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Login.aspx");
        }
    }
}