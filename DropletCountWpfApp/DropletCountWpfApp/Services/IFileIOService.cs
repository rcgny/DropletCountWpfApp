using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.App.Services
{
    public interface IFileIOService
    {

        /// <summary>
        /// Opens the MS file dialog to select Droplet Count Json file for processing
        /// for displaying its contents
        /// </summary>
        /// <returns>Selected full file path</returns>
        string OpenDatafile();
    }
}
