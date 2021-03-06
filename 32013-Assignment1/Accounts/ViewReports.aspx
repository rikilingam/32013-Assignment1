﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewReports.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.ViewReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
            function OpenReceipt(receiptFileName) {
                var path = '<%=ConfigurationManager.AppSettings["ReceiptItemFilePath"].ToString() %>'
            window.open(receiptFileName);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p>
    <asp:Label ID="lblBudgetMessage" runat="server" Height="22px" ></asp:Label>
 </p>
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
                            <asp:ListItem Value="ApprovedBySupervisor">Pending for Accounts Approval</asp:ListItem>
                            <asp:ListItem Value="RejectedByAccounts">Rejected by Accounts</asp:ListItem>
                            <asp:ListItem Value="ApprovedByAccounts">Approved by Accounts</asp:ListItem>
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
                                <th>Report Date: <%# Eval("CreateDate","{0:dd/MM/yyyy}") %></th>
                                <th>Supervisor Processed Date: <%# Eval("ApprovedDate","{0:dd/MM/yyyy}") %> </th>
                                <th>Supervisor: <%# Eval("ApprovedBy.FullName")%> </th>
                                <th>Consultant: <%# Eval("CreatedBy.Fullname") %></th>
                                <th>Department: <%# Eval("ExpenseToDept.DepartmentName") %></th>
                                <th>Status: <%# Eval("Status") %></th>
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
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td></td>
                                        <td><%# Eval("ExpenseDate","{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("Location") %></td>
                                        <td><%# Eval("Description") %></td>
                                        <td><asp:ImageButton ID="btnReceipt" ImageUrl="~/Image/img_pdf_icon.png" runat="server" OnClick="btnReceipt_Click" CommandArgument='<%# Eval("ReceiptFileName") %>' /></td>
                                        <td><%# Eval("AudAmount","{0:c}") %></td>                                        
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
                                <td></td>
                                <td><b>Expense Total:</b></td>
                                <td><b><%# Eval("ExpenseTotal","{0:c}")%></b></td>
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
