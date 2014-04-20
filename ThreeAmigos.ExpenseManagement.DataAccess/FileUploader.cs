using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class FileUploader
    {
        string destinationPath;

        public FileUploader()
        {
            destinationPath = ConfigurationManager.AppSettings["ReceiptItemFilePath"];
        }

        public string Upload(FileUpload fileUpload)
        {
            string newFileName="";

            if (fileUpload.HasFile)
            {
                newFileName = GenerateNewFileName() + ".pdf";
                
                fileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath(destinationPath) + newFileName);
            }

            return newFileName;
        }

        private string GenerateNewFileName()
        {
            Guid guid;
            guid = Guid.NewGuid();

            return guid.ToString();
        }

    }
}
