<%@ Page Title="" Language="C#" MasterPageFile="~/EM_MasterPage.Master" AutoEventWireup="true" CodeBehind="ExpenseForm.aspx.cs" Inherits="_32013_Assignment1.ExpenseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h2>Expense Form</h2>
    </div>


        <div class="form-group">
            <label for="txtExpenseDate" class="col-sm-2 control-label">Date</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtExpenseDate" runat="server" placeholder="Date" CssClass="form-control" Enabled="false"></asp:TextBox>             
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="btnSubmitExpense" runat="server" Text="Submit Report" CssClass="btn btn-primary" />
                <asp:Button ID="btnSaveExpense" runat="server" Text="Save Report" CssClass="btn btn-info" />
            </div>
        </div>

</asp:Content>
