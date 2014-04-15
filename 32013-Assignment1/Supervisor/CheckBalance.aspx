<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckBalance.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Supervisor.CheckBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Check Balances</h3>
        </div>
        <div class="panel-body">
            <div class="container-fluid">

                <div class="row">
                    <div class="col-md-2">

                        <strong>Search Filter:</strong>
                        <asp:DropDownList ID="ddlCheckFilter" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Money Spent">Money Spent</asp:ListItem>
                            <asp:ListItem Value="Money Remaining">Money Remaining</asp:ListItem>
                           </asp:DropDownList>
                        <asp:Button ID="btnBalanceCheck" runat="server" Text="Check" CssClass="btn btn-primary" OnClick="btnBalanceCheck_Click" />
                    </div>

                    <br />
                    <br />

                </div>
                <div class="row">
                    <asp:Label ID="lblMoneyRemaining" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblMoneySpent" runat="server"></asp:Label>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
