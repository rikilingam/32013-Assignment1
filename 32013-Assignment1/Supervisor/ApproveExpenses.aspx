<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ApproveExpenses.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Supervisor.ApproveExpenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Expenses awaiting approval</h3>
        </div>

        <div class="panel-body">
            <div class="container-fluid">
                <div class="row">
                    <asp:Repeater ID="rptExpenseReport" runat="server">
                        <HeaderTemplate>
                            <table class="table">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="success">
                                <th>Report Date: <%# Eval("CreateDate","{0:dd/MM/yyyy}") %></th>
                                <th>Consultant: <%# Eval("CreatedBy.Fullname") %></th>
                                <th>Department: <%# Eval("ExpenseToDept.DepartmentName") %></th>
                                <th>Status: <%# Eval("Status") %></th>
                                <th></th>
                                <th></th>
                                <th></th>

                            </tr>
                            <asp:Repeater ID="rptExpenseItems" DataSource='<%# Eval("ExpenseItems") %>' runat="server" OnItemDataBound="rptExpenseItems_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <th></th>
                                        <th>Expense Date</th>
                                        <th>Location</th>
                                        <th>Description</th>
                                        <th>Receipt</th>
                                        <th>Amount (AUD)</th>
                                        <th>Authorise</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <td></td>
                                    <td><%# Eval("ExpenseDate","{0:dd/MM/yyyy}") %></td>
                                    <td><%# Eval("Location") %></td>
                                    <td><%# Eval("Description") %></td>
                                    <td><asp:ImageButton ID="btnReceipt" ImageUrl="~/Image/img_pdf_icon.png" runat="server" OnClick="btnReceipt_Click" CommandArgument='<%# Eval("ReceiptFileName") %>' /></td>
                                    <td><%# Eval("AudAmount","{0:c}") %></td>
                                    <td></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr class="info">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td><b>Expense Total:</b></td>
                                <td><b><%# Eval("ExpenseTotal","{0:c}")%></b></td>
                                <td><asp:ImageButton ID="btnApprove" ImageUrl="~/Image/img_approve.png" ImageAlign="Middle" runat="server" />&nbsp;&nbsp;<asp:ImageButton ID="btnReject" ImageUrl="~/Image/img_reject.png" ImageAlign="Middle" runat="server" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>


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
                        <asp:Button ID="btnApproveExpense" runat="server" Text="Approve Expense" CommandName="ApproveExpense" CommandArgument='<%#Container.DataItemIndex %>' />
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
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:localDatabase %>" SelectCommand="SELECT [CreateDate] FROM [ExpenseHeader] WHERE ([CreateDate] = @CreateDate)">
            <SelectParameters>
                <asp:Parameter DefaultValue="CreateDate.Month=4" Name="CreateDate" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>
