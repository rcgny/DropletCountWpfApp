using DropletCountWpf.Model.Model;
using System.Data;

namespace DropletCountWpf.UI.Services
{
    public interface IJsonDataService
    {
        /// <summary>
        /// Used to set the file path for the JSON repository code
        /// </summary>
        /// <param name="testDataFilePath"></param>
        void SetFilePath(string testDataFilePath);

        /// <summary>
        /// This returns the deserialized JSON object
        /// </summary>        
        /// <returns>DropletBin</returns>
        DropletBin GetDropletBinFromJson();

        /// <summary>
        /// Converts the deserialized JSON to an in memory data table
        /// for DataGrid.  This method can handle any size JSON wells
        /// array that is a multiple of 8 x n.  For our testing we have 
        /// 2 JSON files with 48 and 96 well counts.
        /// </summary        
        /// <returns>DataTable</returns>
        DataTable GetDataTableFromJson();
    }
}
