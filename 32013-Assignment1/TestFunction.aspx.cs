using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace _32013_Assignment1
{
    public partial class TestFunction : System.Web.UI.Page
    {
        TestFunctionClass test = new TestFunctionClass();
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void lstCurrency_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lstCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            run();
        }

        public void run()
        {
            if (txtCurrency.Text == "")
                MessageBox.Show("Please enter the amount");
            else if (lstCurrency.SelectedItem.Text == "Select Currency")
            {
                MessageBox.Show("Please select the currency");
                lblAUD.Text = "0";
            }
            else
            {
                display();
            }
        }

        public void display()
        {
            string currencyType = lstCurrency.SelectedItem.Text;
            double currencyValue = Convert.ToDouble(txtCurrency.Text);
            lblAUD.Text = test.convertCurrency(currencyType, currencyValue);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (ext == ".pdf")
                {
                    string file = FileUpload1.FileName;
                    FileUpload1.SaveAs(Server.MapPath("~\\Attachments\\") + file);
                }
                else
                {
                    MessageBox.Show("Only pdf files can be uploaded");
                }
            }
            else
            {
                MessageBox.Show("No file selected to upload");
            }
        }
    }
}