using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using System.Web.Security;

namespace ThreeAmigos.ExpenseManagement.UserInterface
{
    public partial class ExpenseForm : System.Web.UI.Page
    {
              
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExpenseReport expenseReport = new ExpenseReport();
                Employee employee = new Employee((Guid)Membership.GetUser().ProviderUserKey);

                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedById = employee.UserId;
                expenseReport.DepartmentId = employee.DepartmentId;
                Session["expenseReport"] = expenseReport;

                txtEmployeeName.Text = employee.FirstName + " " + employee.Surname;
                txtDepartment.Text = employee.DepartmentName; 
                txtExpenseDate.Text = expenseReport.CreateDate.ToString();
                               
            }

        }

        protected void btnAddExpenseItem_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),"ExpenseItemModal","ShowExpenseItemModal();", true);
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            ExpenseReport expenseReport = new ExpenseReport();
            expenseReport = (ExpenseReport)Session["expenseReport"];

            ExpenseItem expenseItem = new ExpenseItem();
            
            expenseItem.ExpenseDate = DateTime.Parse(txtExpenseDate.Text);
            expenseItem.Location = txtItemLocation.Text;
            expenseItem.Description = txtItemDescription.Text;
            expenseItem.Amount = Double.Parse(txtItemAmount.Text);
            expenseItem.Currency = ddlItemCurrency.SelectedValue;
            expenseItem.ReceiptFileName = fileReceipt.FileName;

            expenseReport.AddExpenseItem(expenseItem);

            Session["expenseReport"] = expenseReport;


            gvExpenseItems.DataSource = expenseReport.expenseItems;
            gvExpenseItems.DataBind();



            //bool isNew = true;

            //if(Session["items"]==null)
            //    itemslist=new List<ExpenseItem>();
            //else
            //itemslist=(List<ExpenseItem>) Session["items"];

            //if (isNew)
            //{                
            //    ExpenseItem item = new ExpenseItem();
            //    item.ExpenseDate = DateTime.ParseExact(txtItemDate.Text, "MM/dd/yyyy", null);
            //    item.Location = txtItemLocation.Text;
            //    item.Description = txtItemDescription.Text;
            //    item.Amount = double.Parse(txtItemAmount.Text);
            //    item.Currency = ddlItemCurrency.SelectedItem.Value;
            //    item.AudAmount = CurrencyConverter.ConvertCurrency(item.Currency, item.Amount, Convert.ToDouble(ConfigurationManager.AppSettings["CNY"]), Convert.ToDouble(ConfigurationManager.AppSettings["EUR"]));
            //    item.ReceiptFileName = CheckFile(fileReceipt);
            //    itemslist.Add(item);
            //}
            //Session["items"] = itemslist;
            //clear();
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
            ExpenseReport expenseReport = new ExpenseReport();
            expenseReport = (ExpenseReport)Session["expenseReport"];

            expenseReport.SubmitExpenseReport();

            Response.Redirect("Default.aspx");


            //int CreatedById = (int)(Session["userId"]);
            //DateTime CreateDate = DateTime.Now;
            //DateTime SubmitDate = DateTime.Now;

            //ExpenseReport report = new ExpenseReport();
            //report.AddExpenseReport(CreatedById, CreateDate, SubmitDate);

            //if (Session["ExpenseId"] == null)
            //{
            //    Session["ExpenseId"] = report.FetchExpenseId();
            //}
            //ExpenseItem item = new ExpenseItem();
            //foreach (var i in Session["items"] as IEnumerable<ExpenseItem>)
            //{
            //    item.ExpenseDate = Convert.ToDateTime(i.ExpenseDate);
            //    item.Location = i.Location;
            //    item.Description = i.Description;
            //    item.Amount = Convert.ToDouble(i.Amount);
            //    item.Currency = i.Currency;
            //    item.AudAmount = Convert.ToDouble(i.AudAmount);
            //    item.ReceiptFileName = i.ReceiptFileName;
            //    item.ExpenseHeaderId = (int)Session["ExpenseId"];
            //    item.AddExpenseItem(item.ExpenseDate, item.Location, item.Description, item.Amount, item.Currency, item.AudAmount, item.ReceiptFileName, item.ExpenseHeaderId);
            //}
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