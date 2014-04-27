using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.UserInterface.CustomError
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string genericMessage = "";
            string unhandledError = "The error which occurred was not handled by by the application, please contact support.";

            Exception ex = Server.GetLastError();

            //Exception objError = Server.GetLastError();
            //objError = objError.GetBaseException();

            if (ex!=null)
            {
                lblErrorMessage.Text = ex.GetType().ToString() + "<br/>" +
               ex.InnerException.Message;
                //InnerTrace.Text = ex.InnerException.StackTrace;
            }
            else
            {
                lblErrorMessage.Text = unhandledError;
            }

            // Clear the error from the server.
            Server.ClearError();
        }


    }
}