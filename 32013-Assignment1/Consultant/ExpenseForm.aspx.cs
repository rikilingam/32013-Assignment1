﻿using System;
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
using System.IO;

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
                reportBuilder = (ExpenseReportBuilder)Session["expenseReportBuilder"];
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

            Session["expenseReportBuilder"] = reportBuilder;

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

            if (fileReceipt.HasFile)
            {
                FileUploader fileUploader = new FileUploader();
                expenseItem.ReceiptFileName = fileUploader.Upload(fileReceipt);
            }                     

            reportBuilder.AddExpenseItem(expenseItem);

            Session["expenseReportBuilder"] = reportBuilder;

            gvExpenseItems.DataSource = reportBuilder.expenseReport.ExpenseItems;
            gvExpenseItems.DataBind();

            ClearItemForm();
        }

        protected void btnItemClose_Click(object sender, EventArgs e)
        {
            ClearItemForm();
        }

        protected void btnSubmitExpense_Click(object sender, EventArgs e)
        {

            if (reportBuilder.expenseReport.ExpenseItems.Count > 0)
            {
                reportBuilder.SubmitExpenseReport();

                Response.Redirect("/Default.aspx");
            }

        }

        private void ClearItemForm()
        {
            txtItemDate.Text = "";
            txtExpenseDate.Text = "";
            txtItemLocation.Text = "";
            txtItemDescription.Text = "";
            txtItemAmount.Text = "";
            ddlItemCurrency.SelectedValue = "AUD";
        }


        //protected void cvReceipt_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    if (fileReceipt.PostedFile.ContentLength < 1)
        //    {
        //        args.IsValid = true;
        //    }
        //    else
        //    {
        //        args.IsValid = false;
        //    }
        //}

        protected void lnkReceipt_Click(object sender, EventArgs e)
        {           

            ImageButton btn = (ImageButton)(sender);

            string receiptFileName = btn.CommandArgument.ToString();

            string path = ConfigurationManager.AppSettings["ReceiptItemFilePath"];

            ClientScript.RegisterStartupScript(this.GetType(), "OpenReceipt", "OpenReceipt('"+path+receiptFileName+"');", true);

        }
    }
}