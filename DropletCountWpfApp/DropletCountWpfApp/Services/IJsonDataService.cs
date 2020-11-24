using DropletCountWpf.App.Model;
using DropletCountWpfApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DropletCountWpf.App.Services
{
    public interface IJsonDataService
    {
        /// <summary>
        /// Used to set the file path for the JSON repository code
        /// </summary>
        /// <param name="testDataFilePath"></param>
        void SetFilePath(string testDataFilePath);
        /// <summary>
        /// Provides the data structure with droplet count values for filling the datagrid
        /// </summary>
        /// <returns></returns>
        ObservableCollection<WellForDataGrid96> GetCollection96ForDataGrid();

        /// <summary>
        /// Provides the data structure with droplet count values for filling the datagrid
        /// </summary>
        /// <returns></returns>
        ObservableCollection<WellForDataGrid48> GetCollection48ForDataGrid();
    }
}
