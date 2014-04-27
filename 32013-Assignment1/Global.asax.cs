﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("/CustomError/Error.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }
    }
}