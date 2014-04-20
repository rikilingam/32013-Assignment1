<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ApproveExpenses.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Supervisor.ApproveExpenses"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        Approve Expenses</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:GridView ID="grdExpenseReport" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="ExpenseId" Width="726px">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <br />
                        <asp:Button ID="btnItemDetails" runat="server" Text="Check Items" CommandName="Details" CommandArgument='<%#Container.DataItemIndex %>' />
                        &nbsp;&nbsp;
                        <asp:GridView ID="grdExpenseItems" runat="server">
                        </asp:GridView>
                        <asp:Button ID="btnCancelItems" runat="server" Text="Cancel" Visible="False" CommandName="Cancel" CommandArgument='<%#Container.DataItemIndex %>' />
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnApproveExpense" runat="server" Text="Approve Expense" CommandName="ApproveExpense" CommandArgument='<%#Container.DataItemIndex %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Height="27px" Text="Reject Expense" CommandName="RejectExpense" CommandArgument='<%#Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" Text="Open Receipt" CommandName="OpenReceipt" CommandArgument='<%#Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
