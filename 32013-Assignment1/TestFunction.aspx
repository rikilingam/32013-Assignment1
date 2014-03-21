<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFunction.aspx.cs" Inherits="_32013_Assignment1.TestFunction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            Currency :
            <asp:TextBox ID="txtCurrency" runat="server"></asp:TextBox>
            &nbsp;<asp:DropDownList ID="lstCurrency" runat="server" AutoPostBack="True" DataTextField="Select Currency" OnSelectedIndexChanged="lstCurrency_SelectedIndexChanged" OnTextChanged="lstCurrency_TextChanged">
                <asp:ListItem>Select Currency</asp:ListItem>
                <asp:ListItem>AUD</asp:ListItem>
                <asp:ListItem>CNY</asp:ListItem>
                <asp:ListItem>Euro</asp:ListItem>
            </asp:DropDownList>
            <br />
            AUD&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp; :
            <asp:Label ID="lblAUD" runat="server" Text="0"></asp:Label>
            <br />
            <br />
            <br />
            Attach Receipt :
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
            <br />
        </div>
    </form>
    
</body>
</html>
