<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=TextBox1.ClientID%>');
                dp.datetimepicker({ pickTime: false });
            });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" data-format="DD/MM/YYYY" ></asp:TextBox>
        </div>
    </form>
</body>
</html>
