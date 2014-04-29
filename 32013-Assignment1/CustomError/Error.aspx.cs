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

            string errorMessage;

            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                }

                errorMessage = ex.Message;


                if (ex.Message == "Maximum request length exceeded.")
                {
                    errorMessage = "The file upload failed as the size exceeded the system maximum limit of 4Mb.";
                }
                else
                {
                    errorMessage = ex.Message;
                }
            }

            else
            {
                errorMessage = unhandledError;
            }

            lblErrorMessage.Text = errorMessage;


            // Clear the error from the server.
            Server.ClearError();
        }


    }
}