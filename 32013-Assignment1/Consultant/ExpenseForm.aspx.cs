using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.DataAccess;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using System.Web.Security;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public partial class ExpenseForm : System.Web.UI.Page
    {        
        ExpenseReportBuilder reportBuilder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeExpenseReport();
            }
            else
            {
                reportBuilder = new ExpenseReportBuilder();
                reportBuilder = (ExpenseReportBuilder)Session["expenseReport"];            
            }

        }

        private void InitializeExpenseReport()
        {
            reportBuilder = new ExpenseReportBuilder();
            Employee employee = new Employee();
            EmployeeDAL employeeDAL = new EmployeeDAL();

            employee = employeeDAL.GetEmployee((Guid)Membership.GetUser().ProviderUserKey);

            reportBuilder.expenseReport.CreateDate = DateTime.Now;
            reportBuilder.expenseReport.CreatedBy = employee;
            reportBuilder.expenseReport.ExpenseToDept = employee.Dept;

            Session["expenseReport"] = reportBuilder;

            txtEmployeeName.Text = employee.FirstName + " " + employee.Surname;
            txtDepartment.Text = employee.Dept.DepartmentName;
            txtExpenseDate.Text = reportBuilder.expenseReport.CreateDate.ToString();

        }


        protected void btnAddExpenseItem_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ExpenseItemModal", "ShowExpenseItemModal();", true);
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            ExpenseItem expenseItem = new ExpenseItem();

            expenseItem.ExpenseDate = DateTime.Parse(txtExpenseDate.Text);
            expenseItem.Location = txtItemLocation.Text;
            expenseItem.Description = txtItemDescription.Text;
            expenseItem.Amount = Double.Parse(txtItemAmount.Text);
            expenseItem.Currency = ddlItemCurrency.SelectedValue;
            expenseItem.AudAmount = CurrencyConverter.ConvertToAUD(expenseItem.Currency, expenseItem.Amount);
            expenseItem.ReceiptFileName = fileReceipt.FileName;

            reportBuilder.AddExpenseItem(expenseItem);

            Session["expenseReport"] = reportBuilder;


            gvExpenseItems.DataSource = reportBuilder.expenseReport.ExpenseItems;
            gvExpenseItems.DataBind();

        }

        public string CheckFile(FileUpload filename)
        {
            string ext = System.IO.Path.GetExtension(filename.FileName);
            string file = filename.FileName;
            filename.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ReceiptItemFilePath"]) + file);
            return file;
        }

        protected void btnSubmitExpense_Click(object sender, EventArgs e)
        {            

            if (reportBuilder.expenseReport.ExpenseItems.Count > 0)
            {
                reportBuilder.SubmitExpenseReport();

                Response.Redirect("/Default.aspx");
            }

        }

        public void clear()
        {
            txtItemDate.Text = string.Empty;
            txtExpenseDate.Text = string.Empty;
            txtItemLocation.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            txtItemAmount.Text = string.Empty;
            ddlItemCurrency.SelectedValue = "AUD";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}