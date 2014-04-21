<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckExpenseApproved.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.CheckExpenseApproved" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Amount of expenses Approved"></asp:Label>
&nbsp;by each supervisor:</div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId" DataSourceID="SqlAmountApproved">
            <Columns>
                <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" InsertVisible="False" ReadOnly="True" SortExpression="EmployeeId" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                <asp:BoundField DataField="Total Amount" HeaderText="Total Amount" ReadOnly="True" SortExpression="Total Amount" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlAmountApproved" runat="server" ConnectionString="<%$ ConnectionStrings:localDatabase %>" SelectCommand="SELECT A.EmployeeId,  A.FirstName,  A.Surname, SUM(C.AudAmount) AS &quot;Total Amount&quot; FROM Employee AS A INNER JOIN ExpenseHeader AS B ON A.UserId = B.ApprovedById INNER JOIN ExpenseItem AS C ON B.ExpenseId = C.ExpenseHeaderId 
WHERE (B.Status = 'ApprovedBySupervisor') OR (B.Status = 'ApprovedByAccount')
GROUP BY A.EmployeeId,  A.FirstName,  A.Surname
"></asp:SqlDataSource>
        <br />
        Total amount of expenses approved for the entire company:<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlAmountProcessedAll">
            <Columns>
                <asp:BoundField DataField="Total Amount" HeaderText="Total Amount" ReadOnly="True" SortExpression="Total Amount" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlAmountProcessedAll" runat="server" ConnectionString="<%$ ConnectionStrings:localDatabase %>" SelectCommand="SELECT SUM(B.AudAmount) AS &quot;Total Amount&quot;
FROM ExpenseHeader AS A INNER JOIN ExpenseItem AS B ON A.ExpenseId = B.ExpenseHeaderId
WHERE (A.Status = 'ApprovedBySupervisor') OR (A.Status = 'ApprovedByAccount')"></asp:SqlDataSource>
    </form>
</body>
</html>
