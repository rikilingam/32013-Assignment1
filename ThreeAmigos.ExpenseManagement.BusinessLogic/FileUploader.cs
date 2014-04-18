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
            const int DEFAULT_FILESIZE =1;

            long sourceFileSize = -1;
            long maxFileSize = DEFAULT_FILESIZE;
            bool isValid = false;

            if (CheckFileExist(filepath))
            {
                FileInfo fileInfo = new FileInfo(filepath);
                sourceFileSize = fileInfo.Length;

                if (long.TryParse(ConfigurationManager.AppSettings["MaxUploadFileSize"], out maxFileSize) && sourceFileSize < maxFileSize)
                {
                    isValid = true;
                }
            }
            
            return isValid;
        }
    }
}
