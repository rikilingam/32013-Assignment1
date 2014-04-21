<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBalance.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.CheckBalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Check Monthly Budget Remain</div>
        <asp:DropDownList ID="ddlFilter" runat="server" Height="25px" Width="216px">
            <asp:ListItem>Money spent</asp:ListItem>
            <asp:ListItem>Money remaining</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        <br />
        <asp:Label ID="lblMoneySpent" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblMoneyRemaining" runat="server" Text="Label"></asp:Label>
        <br />
    </form>
</body>
</html>
