<%@ Page Title="" Language="C#" MasterPageFile="~/EM_MasterPage.Master" AutoEventWireup="true" CodeBehind="ExpenseForm.aspx.cs" Inherits="_32013_Assignment1.ExpenseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Expense Form</h3>
        </div>

        <div class="panel-body">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtExpenseDate" class="control-label">Date</label>
                            <asp:TextBox ID="txtExpenseDate" runat="server" placeholder="Date" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2">
                        <div class="form-group">

                            <asp:Button ID="btnSubmitExpense" runat="server" Text="Submit Report" CssClass="btn btn-primary" />
                            <asp:Button ID="btnSaveExpense" runat="server" Text="Save Report" CssClass="btn btn-info" />
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>


</asp:Content>
