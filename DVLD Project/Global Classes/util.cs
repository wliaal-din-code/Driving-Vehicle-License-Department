using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project_Wla.Global_Classes
{
    public class clsutil
    {
        public static string GenerateGUID()
        {
            Guid Neguid = new Guid();

            Neguid = Guid.NewGuid();

            return Neguid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string exten = fi.Extension;
            return GenerateGUID() + exten;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:\DVLD-People-Images\";

            if(!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationfile = DestinationFolder +  ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationfile, true);
            }
            catch(IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = destinationfile;
            return true;
        }
    }
}
