<%@ Page Title="" Language="C#" MasterPageFile="~/EM_Default_MasterPage.Master" AutoEventWireup="true" CodeBehind="ExpenseForm.aspx.cs" Inherits="ThreeAmigos.ExpenseManagement.UserInterface.ExpenseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowExpenseItemModal() {
            $('#ExpenseItemModal').modal('show');
        }

        function HideExpenseItemModal() {
            $('#ExpenseItemModal').modal('hide');
        }
    </script>

    <script type="text/javascript">
        var today = new Date();
        $(document).ready(function () {
            var dp = $('#<%=txtItemDate.ClientID%>');
            dp.datetimepicker({
                pickTime: false,
                autoclose: true,

            });
        });

    </script>
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
                            <label for="txtExpenseDate" class="control-label">Report Date</label>
                            <asp:TextBox ID="txtExpenseDate" runat="server" placeholder="Date" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <hr />
                </div>

                <!-- This row contains the expense items -->

                <div class="row">
                    <div class="col-md-7">
                        <asp:GridView ID="gvExpenseItems" runat="server" AutoGenerateColumns="False" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="ExpenseDate" DataFormatString="{0:d}" HeaderText="Date" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount" />
                                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                <asp:BoundField DataField="AudAmount" DataFormatString="{0:c}" HeaderText="AUD Amount" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <hr />
                </div>

                <!-- This row is the buttons  data-toggle="modal" data-target="#myModal" -->
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:Button ID="btnAddExpenseItem" runat="server" Text="Add Item" CssClass="btn btn-primary" OnClick="btnAddExpenseItem_Click" />
                            <asp:Button ID="btnSubmitExpense" runat="server" Text="Submit Report" CssClass="btn btn-success" OnClick="btnSubmitExpense_Click" />
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <!-- modal to add items -->
    <div class="modal fade" id="ExpenseItemModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Add Expense Item</h4>
                </div>
                <div class="modal-body">

                    <!-- Expense items -->
                    <div class="row">
                        <div class="col-md-3 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemDate" class="control-label">Date</label>
                                <asp:TextBox ID="txtItemDate" runat="server" placeholder="Item date" CssClass="form-control" Enabled="true" data-format="DD/MM/YYYY"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemLocation" class="control-label">Location</label>
                                <asp:TextBox ID="txtItemLocation" runat="server" placeholder="Location" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                        </div>


                    </div>
                    <div class="row">

                        <div class="col-md-6 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemDescription" class="control-label">Description</label>
                                <asp:TextBox ID="txtItemDescription" runat="server" placeholder="Description" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-3 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemAmount" class="control-label">Amount</label>
                                <asp:TextBox ID="txtItemAmount" runat="server" placeholder="Amount" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3 em-col-md-offset-2">
                            <div class="form-group">
                                <label for="ddlItemCurrency" class="control-label">Currency</label>
                                <asp:DropDownList ID="ddlItemCurrency" runat="server" placeholder="Currency" CssClass="form-control" Enabled="true">
                                    <asp:ListItem>AUD</asp:ListItem>
                                    <asp:ListItem>CNY</asp:ListItem>
                                    <asp:ListItem>EUR</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-md-offset-1">
                            <div class="form-group">
                                <label for="fileReceipt" class="control-label">Receipt</label>
                                <asp:FileUpload ID="fileReceipt" runat="server" CssClass="form-control"></asp:FileUpload>

                            </div>
                        </div>
                    </div>
                </div>


                <div class="modal-footer">
                    <asp:Button ID="btnAddItem" runat="server" Text="Add to Report" CssClass="btn btn-primary" OnClick="btnAddItem_Click" />
                    <asp:Button ID="btnItemClose" runat="server" Text="Close" CssClass="btn btn-default" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>
</asp:Content>
