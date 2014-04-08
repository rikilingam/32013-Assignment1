<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewMyExpenses.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Consultant.ViewMyExpenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">View My Expenses</h3>
        </div>
        <div class="panel-body">
            <div class="container-fluid">

                <div class="row">
                    <div class="col-md-2">

                        <strong>Search Filter:</strong>
                        <asp:DropDownList ID="ddlSearchFilter" runat="server" CssClass="form-control">
                            <asp:ListItem>All Submitted</asp:ListItem>
                            <asp:ListItem>All Approved</asp:ListItem>
                            <asp:ListItem>Pending Approval</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearchExpenses" runat="server" Text="Search" CssClass="btn btn-primary" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
