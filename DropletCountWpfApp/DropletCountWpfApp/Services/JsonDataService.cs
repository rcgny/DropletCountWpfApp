using DropletCountWpf.App.Model;
using DropletCountWpfApp.DAL.Repository;
using DropletCountWpfApp.Model;
using DropletCountWpfApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.App.Services
{
    public class JsonDataService: IJsonDataService
    {
        #region Data Fields
        IJsonRepository _repository;
        private DropletBin _dropletBin;
        private string _defaultFilePath = @"C:\DropletData\PlateDropletInfo.json";
        private List<Well> _wells = new List<Well>();
        private ObservableCollection<WellForDataGrid96> _wellsForDataGrid96 = new ObservableCollection<WellForDataGrid96>();
        private ObservableCollection<WellForDataGrid48> _wellsForDataGrid48 = new ObservableCollection<WellForDataGrid48>();
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
            if(!string.IsNullOrEmpty(testDataFilePath))
            {
                _defaultFilePath = testDataFilePath;
            }                
        }


        public ObservableCollection<WellForDataGrid96> GetCollection96ForDataGrid()
        {

            try
            {
                var DropletBin = GetDropletBin(_defaultFilePath);                

                ClearCollections();

                foreach (var well in DropletBin.PlateDropletInfo.DropletInfo.Wells)
                {
                    _wells.Add(well);
                }

                // See Well class for property that we sort on: WellIndex
                _wells.Sort();

                string[] strArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
                int index = 0;
                for (int i = 0; i < 8; i++)
                {
                    _wellsForDataGrid96.Add(new WellForDataGrid96()
                    {
                        Bin = strArray[i],
                        O1 = _wells[index++].DropletCount,
                        O2 = _wells[index++].DropletCount,
                        O3 = _wells[index++].DropletCount,
                        O4 = _wells[index++].DropletCount,
                        O5 = _wells[index++].DropletCount,
                        O6 = _wells[index++].DropletCount,
                        O7 = _wells[index++].DropletCount,
                        O8 = _wells[index++].DropletCount,
                        O9 = _wells[index++].DropletCount,
                        O10 = _wells[index++].DropletCount,
                        O11 = _wells[index++].DropletCount,
                        O12 = _wells[index++].DropletCount
                    });
                }
            }
            catch (Exception ex)
            {
                //TODO: Logging
            }

            return _wellsForDataGrid96;
        }

        public ObservableCollection<WellForDataGrid48> GetCollection48ForDataGrid()
        {

            try
            {
                var DropletBin = GetDropletBin(_defaultFilePath);
                ClearCollections();

                foreach (var well in DropletBin.PlateDropletInfo.DropletInfo.Wells)
                {
                    _wells.Add(well);
                }

                // See Well class for property that we sort on: WellIndex
                _wells.Sort();

                string[] strArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
                int index = 0;

                for (int i = 0; i < 8; i++)
                {
                    _wellsForDataGrid48.Add(new WellForDataGrid48()
                    {
                        Bin = strArray[i],
                        O1 = _wells[index++].DropletCount,
                        O2 = _wells[index++].DropletCount,
                        O3 = _wells[index++].DropletCount,
                        O4 = _wells[index++].DropletCount,
                        O5 = _wells[index++].DropletCount,
                        O6 = _wells[index++].DropletCount
                    });
                }
            }
            catch (Exception ex)
            {
                //TODO: Logging
            }

            return _wellsForDataGrid48;
        }

        #endregion

        #region Helpers
        private DropletBin GetDropletBin(string filePath)
        {
            return _repository.GetDropletBin(filePath);
        }

        private void ClearCollections()
        {
            _wells.Clear();
            _wellsForDataGrid96.Clear();            
            _wellsForDataGrid48.Clear();
        }


        #endregion


    }
}
