﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewReports.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Supervisor.ViewMyExpenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">View Reports</h3>
        </div>
        <div class="panel-body">
            <div class="container-fluid">

                <div class="row">
                    <div class="col-md-2">

                        <strong>Search Filter:</strong>
                        <asp:DropDownList ID="ddlSearchFilter" runat="server" CssClass="form-control">
                            <asp:ListItem Value="ApprovedBySupervisor">All Approved</asp:ListItem>
                            <asp:ListItem Value="RejectedBySupervisor">All Rejected</asp:ListItem>
                            <asp:ListItem Value="RejectedByAccountant">Rejected By Accounts</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearchExpenses" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearchExpenses_Click" />
                    </div>

                </div>
                <div class="row">
                    <hr />
                </div>
                <div class="row" id="divDisplayExpenseReports" runat="server">
                    <asp:Repeater ID="rptExpenseReport" runat="server">
                        <HeaderTemplate>
                            <table class="table">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="success">
                                <th>Report Date: <%# Eval("CreateDate") %></th>
                                <th>Consultant: <%# Eval("CreatedBy.Fullname") %></th>
                                <th>Department: <%# Eval("ExpenseToDept.DepartmentName") %></th>
                                <th>Status: <%# Eval("Status") %></th>
                                <th></th>
                                <th></th>
                            </tr>
                            <asp:Repeater ID="rptExpenseItems" DataSource='<%# Eval("ExpenseItems") %>' runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th></th>
                                        <th>Expense Date</th>
                                        <th>Location</th>
                                        <th>Description</th>
                                        <th>Receipt</th>
                                        <th>Amount (AUD)</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <th></th>
                                    <td><%# Eval("ExpenseDate") %></td>
                                    <td><%# Eval("Location") %></td>
                                    <td><%# Eval("Description") %></td>
                                    <td><%# Eval("ReceiptFileName") %></td>
                                    <td><%# Eval("AudAmount") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr class="info">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
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