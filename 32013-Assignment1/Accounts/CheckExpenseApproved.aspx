<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckExpenseApproved.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.CheckExpenseApproved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblBudgetMessage" runat="server" Height="22px" Width="302px"></asp:Label>
                <br />
                <asp:Label ID="lblMoneyRemaining" runat="server" Height="22px" Width="302px"></asp:Label>
                <br />
                </div>
                <br />
        </div>

        <div class="panel-heading">
            <h3 class="panel-title">Expenses approved by supervisor</h3>
        </div>

        <div class="panel-body">
            <div class="container-fluid">
                <div class="row">
                    <asp:Repeater ID="rptExpenseReport" runat="server" onItemDataBound="FormatRepeaterRow" >
                        <HeaderTemplate>
                            <table class="table">
                        </HeaderTemplate>
                        <ItemTemplate> 
                            <tr class="success">
                                <th>Department: <asp:Label ID="lblDepartment" runat ="server" Text = '<%# Eval("ExpenseToDept.DepartmentName") %>' /> </th>
                                <th>Supervisor: <asp:Label ID="lblSupervisor" runat ="server" Text = '<%# Eval("ApprovedBy.Fullname") %>' /> </th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th><b>Expense Total:</b></th>
                                <td><b><asp:Label ID="Label1" runat="server" Text='<%# Eval("ExpenseTotal","{0:c}")%>' /> </b></td>
                                <th></th>
                            </tr>

                            <tr class="info">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
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
</asp:Content>