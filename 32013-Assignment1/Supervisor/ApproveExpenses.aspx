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
        <asp:GridView ID="grdExpenseReport" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="ExpenseId" Width="904px" BorderStyle="None">
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
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:localDatabase %>" SelectCommand="SELECT [CreateDate] FROM [ExpenseHeader] WHERE ([CreateDate] = @CreateDate)">
            <SelectParameters>
                <asp:Parameter DefaultValue="CreateDate.Month=4" Name="CreateDate" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
