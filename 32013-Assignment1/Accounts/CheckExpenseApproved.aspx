<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckExpenseApproved.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.CheckExpenseApproved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
     <asp:Label ID="lblBudgetMessage" runat="server" Height="22px" Width="972px"></asp:Label>
    </p>
    <div class="panel panel-primary">
        <div class="row">
           <div class="col-md-2">
           </div>
        </div>

        <div class="panel-heading">
            <h3 class="panel-title">Expense reports approved by supervisor</h3>
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
                                <th><b>Supervisor: <asp:Label ID="lblDepartment" runat ="server" Text = '<%# Eval("Fullname") %>' /> </b></th>
                                <th><b>Department: <asp:Label ID="Label1" runat ="server" Text = '<%# Eval("Dept.DepartmentName") %>' /></b> </th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>

                            <tr class="info">
                                <td><b>Total of expense reports: <asp:Label ID="lblSupervisor" runat ="server" Text = '<%# Eval("AmountApproved") %>' /> </b></td>
                                <td><b>Total expense: <asp:Label ID="Label2" runat ="server" Text = '<%# Eval("ExpenseApproved","{0:c}") %>' /> </b></td>
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