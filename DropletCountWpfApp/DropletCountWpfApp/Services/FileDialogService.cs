using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DropletCountWpf.UI.Services
{
    public class FileIOService : IFileIOService
    {
        // File path as setup by the WebRanger Installer, and available in registry.
        private string _datafolder = @"C:\DropletData";

        public FileIOService()
        {

        }

        public string OpenDatafile()
        {
            string filePath = String.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Logs (*.json) | *.json";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = _datafolder;

            if (openFileDialog.ShowDialog() == true)
            {               
                filePath = openFileDialog.FileName;
            }
            return filePath;

        }     
    }
}
