<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ProcessExpenses.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.ProcessExpenses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        Process Expenses</p>
<asp:GridView ID="grdProcessExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="ExpenseId" DataSourceID="SqlExpenseDetail" Width="275px">
    <Columns>
        <asp:BoundField DataField="ExpenseId" HeaderText="ExpenseId" InsertVisible="False" ReadOnly="True" SortExpression="ExpenseId" />
        <asp:BoundField DataField="SubmitDate" HeaderText="SubmitDate" SortExpression="SubmitDate" />
        <asp:BoundField DataField="ApprovedDate" HeaderText="ApprovedDate" SortExpression="ApprovedDate" />
        <asp:BoundField DataField="CreatedById" HeaderText="CreatedById" SortExpression="CreatedById" />
        <asp:BoundField DataField="ApprovedById" HeaderText="ApprovedById" SortExpression="ApprovedById" />
        <asp:BoundField DataField="Expr1" HeaderText="Expr1" SortExpression="Expr1" ReadOnly="True" />
        <asp:ButtonField ButtonType="Button" CommandName="Update" HeaderText="Process" ShowHeader="True" Text="Approve" />
        <asp:ButtonField ButtonType="Button" CommandName="Update" HeaderText="Reject" ShowHeader="True" Text="Reject" />
    </Columns>
</asp:GridView>
    <asp:SqlDataSource ID="SqlExpenseDetail" runat="server" ConnectionString="<%$ ConnectionStrings:localDatabase %>" SelectCommand="SELECT A.ExpenseId, A.SubmitDate, A.ApprovedDate, A.CreatedById, A.ApprovedById, SUM(B.AudAmount) AS Expr1
FROM ExpenseHeader AS A INNER JOIN ExpenseItem AS B ON A.ExpenseId = B.ExpenseHeaderId
WHERE A.Status = 'ApprovedBySupervisor'
GROUP BY A.ExpenseId, A.SubmitDate, A.ApprovedDate, A.CreatedById, A.ApprovedById"></asp:SqlDataSource>
</asp:Content>
