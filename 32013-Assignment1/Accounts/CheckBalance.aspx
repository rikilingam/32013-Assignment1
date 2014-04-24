<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckBalance.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Accounts.CheckBalance" %>
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

                    <asp:Label ID="lblMoneySpent" runat="server" Height="22px" Width="302px"></asp:Label>
                        <br />
                        <br />
                    <asp:Label ID="lblMoneyRemaining" runat="server" Height="22px" Width="302px"></asp:Label>
                    <br />
                    </div>

                    <br />

                </div>
            </div>
        </div>
    </div>
</asp:Content>