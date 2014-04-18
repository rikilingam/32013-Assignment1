using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    public class FileUploader
    {
        string destinationPath;

        public FileUploader()
        {
            destinationPath = ConfigurationManager.AppSettings["ReceiptFilePath"];
        }

        public void Upload(string sourceFile, string newfilename)
        {
            if (CheckOutputPathExists() && CheckFileExist(sourceFile))
            {
                File.Copy(sourceFile, destinationPath + newfilename);
            }
        }

        private bool CheckFileExist(string sourceFile)
        {
            return File.Exists(sourceFile);
        }

        private bool CheckOutputPathExists()
        {
            return Directory.Exists(destinationPath);

        }

        public bool CheckFileSize(string filepath)
        {
            long fileSize = -1;
            int maxFileSize = 1;

            if (CheckFileExist(filepath))
            {
                FileInfo fileInfo = new FileInfo(filepath);
                fileSize = fileInfo.Length;
            }

            if (Int32.TryParse(ConfigurationManager.AppSettings["MaxFileSize"], out maxFileSize))
            {
                if (fileSize < maxFileSize)
                    return true;
            }
            else
            {
                return false;
            }



        }
    }
}
