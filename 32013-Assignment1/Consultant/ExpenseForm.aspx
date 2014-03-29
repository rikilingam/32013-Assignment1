<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ExpenseForm.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.ExpenseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Expense Form</h3>
        </div>

        <div class="panel-body">
            <div class="container-fluid">
                <!-- This is the expense header -->
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="txtEmployeeName" class="control-label">Employee name</label>
                            <asp:TextBox ID="txtEmployeeName" runat="server" placeholder="Employee Name" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2 em-col-md-offset-2">
                        <div class="form-group">
                            <label for="txtDepartment" class="control-label">Department</label>
                            <asp:TextBox ID="txtDepartment" runat="server" placeholder="Department" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2 em-col-md-offset-2">
                        <div class="form-group">
                            <label for="txtExpenseDate" class="control-label">Date</label>
                            <asp:TextBox ID="txtExpenseDate" runat="server" placeholder="Date" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- This row contains the expense items -->
                
                        <table class="table">
                            <tr><td>Expense items to go here</td></tr>
                        </table>

                <!-- This row is the buttons -->
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:Button ID="btnAddItem" runat="server" Text="Add Item" CssClass="btn btn-primary" />
                            <asp:Button ID="btnSubmitExpense" runat="server" Text="Submit Report" CssClass="btn btn-success" />
                            <asp:Button ID="btnSaveExpense" runat="server" Text="Save Report" CssClass="btn btn-info" />
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>


</asp:Content>
