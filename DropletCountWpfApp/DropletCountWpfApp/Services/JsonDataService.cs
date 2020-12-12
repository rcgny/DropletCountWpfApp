using DropletCountWpf.DAL.Repository;
using DropletCountWpf.Model;
using DropletCountWpf.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DropletCountWpf.UI.Services
{
    public class JsonDataService : IJsonDataService
    {
        #region Data Fields
        IJsonRepository _repository;
        private string _defaultFilePath = @"C:\DropletData\PlateDropletInfo.json";
        private List<Well> _wells = new List<Well>();
        private const int NumberOfRowsForTable = 8;  // Per requirements doc every table displaying droplet count from JSON 
                                                     // will be organized as 8 x xx
        #endregion

        #region Properties
        #endregion


        #region Ctor

        public JsonDataService(IJsonRepository repository)
        {
            _repository = repository;
        }


        #endregion


        #region IF Implementation

        public void SetFilePath(string testDataFilePath)
        {
            if (!string.IsNullOrEmpty(testDataFilePath))
            {
                _defaultFilePath = testDataFilePath;
            }
        }

        public DropletBin GetDropletBinFromJson()
        {
            return _repository.GetDropletBin(_defaultFilePath);
        }


        public DataTable GetDataTableFromJson()
        {
            DropletBin dropletBin = null;
            List<Well> wells = null;  // Rep. the array of droplet counts.
            DataTable table = null;

            try
            {
                // Get the deserialized JSON.
                dropletBin = GetDropletBinFromJson();

                // Need to sort the JSON droplet count array, so we put them in a list
                foreach (var well in dropletBin.PlateDropletInfo.DropletInfo.Wells)
                {
                    _wells.Add(well);
                }
                _wells.Sort(); // See Well class for property that we sort on: WellIndex           

                // Populate an in memory data table for easy display by consuming dataGrid
                table = new DataTable();


                // Setup columns
                var columnCount = _wells.Count / NumberOfRowsForTable;
                string[] columnNames = new string[columnCount + 1];

                columnNames[0] = "-";
                for (int i = 1; i < columnCount + 1; i++)
                {
                    columnNames[i] = i.ToString();
                }

                foreach (var cn in columnNames)
                    table.Columns.Add(cn, typeof(string));

                // Organize the wells array into 8 x xx rows
                var rows = _wells.Select(i => i.DropletCount).ToArray();

                // This list is from the WellNames of each well array item. See the original JSON.
                List<char> ids = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
                int itemsPerRow = 0;
                for (int i = 0; i < NumberOfRowsForTable; i++)
                {
                    object[] values = new object[columnNames.Length];
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (j == 0) // add Id
                        {
                            values[j] = ids.First<char>();
                            ids.RemoveAt(j);
                        }
                        values[j + 1] = rows[j + itemsPerRow];
                    }
                    itemsPerRow += columnCount;
                    table.Rows.Add(values);
                }
                ClearCollections();
            }
            catch (Exception ex)
            {
                //todo: Logging
            }
            return table;
        }

        #endregion

        #region Helpers


        private void ClearCollections()
        {
            _wells.Clear();
        }


        #endregion


    }
}
