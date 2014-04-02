<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

          <script src="Scripts/jquery-1.9.0.js"></script>
    <script src="Scripts/bootstrap.js"></script>
      <script src="Scripts/bootstrap-datetimepicker.min.js"></script>

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <title></title>
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
