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
            //string genericMessage = "";
            string unhandledError = "The error which occurred was not handled by the application, please contact support.";
            
            Exception ex = Server.GetLastError();
            
            if (ex != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                }

                lblErrorMessage.Text = ex.Message;      
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