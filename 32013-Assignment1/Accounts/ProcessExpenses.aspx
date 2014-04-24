﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ProcessExpenses.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.ProcessExpenses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <p>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </p>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Expenses awaiting approval</h3>
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
                                <th>Report Date: <asp:Label ID="lblDate" runat ="server" Text = '<%# Eval("CreateDate","{0:dd/MM/yyyy}") %>' /> </th>
                                <th>Consultant: <asp:Label ID="lblConsultant" runat ="server" Text = '<%# Eval("CreatedBy.Fullname") %>' /> </th>
                                <th>Supervisor: <asp:Label ID="lblSupervisor" runat ="server" Text = '<%# Eval("CreatedBy.Fullname") %>' /> </th>
                                <th>Department: <asp:Label ID="lblDepartment" runat ="server" Text = '<%# Eval("ExpenseToDept.DepartmentName") %>' /> </th>
                                <th>Status: <asp:Label ID="lblStatus" runat ="server" Text = '<%# Eval("Status") %>' /> </th>
                                <th><asp:Label ID="lblDepartmentId" Visible ="false" runat="server" Text='<%#Eval("DepartmentId")%>'/> </th> 
                                <th><asp:Label ID="lblExpense" Visible ="false" runat="server" Text='<%# Eval("ExpenseTotal")%>' /></th>
                                <th></th>
                            </tr>
                            <asp:Repeater ID="rptExpenseItems" DataSource='<%# Eval("ExpenseItems") %>' runat="server" OnItemDataBound="rptExpenseItems_ItemDataBound" >
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
                                <td><b><asp:Label ID="lblExp" runat="server" Text='<%# Eval("ExpenseTotal","{0:c}")%>' /> </b></td>
                                <td><asp:ImageButton ID="btnApprove" ImageUrl="~/Image/img_approve.png" ImageAlign="Middle" runat="server" CommandName="ApproveExpense" CommandArgument='<%#Eval("ExpenseId") + ","+Eval("ExpenseTotal") %>' OnClick="btnApprove_Click" />&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnReject" ImageUrl="~/Image/img_reject.png" ImageAlign="Middle" runat="server" CommandName="RejectExpense" CommandArgument='<%# Eval("ExpenseId") %>' OnClick="btnReject_Click" /></td>
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






</asp:Content>
