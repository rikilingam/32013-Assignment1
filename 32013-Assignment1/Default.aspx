<%@ Page Title="" Language="C#" MasterPageFile="~/EM_NoMembership_MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        User <asp:LoginName ID="LoginName1" runat="server" /> is not a member of any roles...
    </p>
</asp:Content>
