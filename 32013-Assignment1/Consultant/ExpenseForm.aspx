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
    <script>
        function OpenReceipt(receiptFileName)
        {
            var path = '<%=ConfigurationManager.AppSettings["ReceiptItemFilePath"].ToString() %>'
            window.open(receiptFileName);    
        }
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
                        <asp:GridView ID="gvExpenseItems" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="gvExpenseItems_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ExpenseDate" DataFormatString="{0:d}" HeaderText="Date" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount" />
                                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                <asp:BoundField DataField="AudAmount" DataFormatString="{0:c}" HeaderText="AUD Amount" />
                                <asp:TemplateField HeaderText="Receipt">
                                    <ItemTemplate>                                        
                                        <asp:ImageButton ID="btnReceipt" ImageUrl="~/Image/img_pdf_icon.png" runat="server" OnClick="lnkReceipt_Click" CommandArgument='<%#Bind("ReceiptFileName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
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
                                <label for="txtItemDate" class="control-label">Date</label><asp:RequiredFieldValidator ID="rfvItemDate" runat="server" ValidationGroup="vgExpenseItem" ControlToValidate="txtItemDate" Text="*"  ErrorMessage="Date of expense required" ></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexItemDate" runat="server" Text="*" ValidationGroup="vgExpenseItem" ControlToValidate="txtItemDate"  ErrorMessage="Invalid date for expense item" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((20[0-9][0-9]))|((((0[1-9])|(1\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31/((0[13578])|(1[02])))|((29|30)/((0[1,3-9])|(1[0-2])))))/((19[0-9][0-9]))|(29/02/20(([02468][048])|([13579][26])))|(29/02/19(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtItemDate" runat="server" placeholder="Item date" CssClass="form-control" Enabled="true" data-date-format="DD/MM/YYYY"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemLocation" class="control-label">Location</label>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Location of expense" ControlToValidate="txtItemLocation" ValidationGroup="vgExpenseItem" Text="*"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtItemLocation" runat="server" placeholder="Location" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                        </div>


                    </div>
                    <div class="row">

                        <div class="col-md-6 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemDescription" class="control-label">Description</label><asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description of expense" ControlToValidate="txtItemDescription" Text="*" ValidationGroup="vgExpenseItem"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtItemDescription" runat="server" placeholder="Description" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-3 col-md-offset-1">
                            <div class="form-group">
                                <label for="txtItemAmount" class="control-label">Amount</label>
                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Expense amount" ControlToValidate="txtItemAmount" ValidationGroup="vgExpenseItem" Text="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="amountValidator" runat="server" ValidationGroup="vgExpenseItem" ErrorMessage="Invalid expense amount" ValidationExpression="^[1-9]\d*(\.\d+)?$" ControlToValidate="txtItemAmount">*</asp:RegularExpressionValidator>
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
                                (PDF files only)
                                <asp:RegularExpressionValidator ID="revReceipt" runat="server" ErrorMessage="Invalid file type" ControlToValidate="fileReceipt" ValidationExpression="^.+\.((PDF)|(pdf)|(Pdf)|(PDf)|(pDF)|(pdF))$" ValidationGroup="vgExpenseItem" Text="*"></asp:RegularExpressionValidator>
                                <asp:FileUpload ID="fileReceipt" runat="server" CssClass="form-control"></asp:FileUpload>

                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:ValidationSummary ID="expenseItemValidationSummary" ValidationGroup="vgExpenseItem" runat="server" HeaderText="Please check the following;" />
                        </div>
                    </div>
                </div>


                <div class="modal-footer">
                    <asp:Button ID="btnAddItem" runat="server" Text="Add to Report" CssClass="btn btn-primary" ValidationGroup="vgExpenseItem" OnClick="btnAddItem_Click" />
                    <asp:Button ID="btnItemClose" runat="server" Text="Close" CssClass="btn btn-default" OnClick="btnItemClose_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>

</asp:Content>
