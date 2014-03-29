<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Content/EM_StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" role="form">
        <div class="login_main">

            <asp:Login ID="loginMain" runat="server">
                <LayoutTemplate>
                    <h4>Expense Management System</h4>
                    <div class="container-fluid">
                        
                        <div class="form-group">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>&nbsp;
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>&nbsp;<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                            <br />
                        </div></div>
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><br />
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="btn btn-primary" />
                  
                </LayoutTemplate>
            </asp:Login>

        </div>

    </form>
</body>
</html>
