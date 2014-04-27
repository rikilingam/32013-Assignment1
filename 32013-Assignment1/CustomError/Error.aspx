<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.CustomError.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>System error</h2>
    <p>You have arrived at this page because an application error has occured, we apologise for the inconvenience.<br />The information below can be used to troubleshoot the problem or report the fault to the system administator:</p>

    <b>Error Message:</b><br />
    <asp:Label runat="server" ID="lblErrorMessage" Text=""></asp:Label>

</asp:Content>
