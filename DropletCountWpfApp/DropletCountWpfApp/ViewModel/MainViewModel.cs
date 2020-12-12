using DropletCountWpf.Model.Model;
using DropletCountWpf.UI.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Data;
namespace DropletCountWpf.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Data Fields

        private IJsonDataService _jsonDataService;
        private IFileIOService _fileIOService;
        private string _filePath = string.Empty;
        private const string SelectedFilePropertyName = "SelectedFile";
        private const string TotalWellCountPropertyName = "TotalWellCount";
        private const string TotalLowDropletCountPropertyName = "TotalLowDropletCount";
        private const string DropletThresholdPropertyName = "DropletThreshold";
        private const string WellsTablePropertyName = "WellsTable";
        private const int DefaultThreshold = 100;

        private int _previousDropletThreshold = 100; // Set to default value, the same as current below.
        private DropletBin _dropletBin;
        #endregion

        #region Properties     


        // rcg: In order to be able to replace the entire collection, in the command handler below, AND trigger the binding, 
        //      had to add a backing variable, since replacing the instance of the actual public ObservableCollection property
        //      breaks the original xaml binding...
        // private ObservableCollection<WellForDataGrid96> _wellsForDataGrid96 = new ObservableCollection<WellForDataGrid96>();
        // public ObservableCollection<WellForDataGrid96> WellsForDataGrid96
        //{
        //     get { return _wellsForDataGrid96; }
        //     set
        //     {
        //         _wellsForDataGrid96 = value;
        //         RaisePropertyChanged(WellsForDataGrid96PropertyName);
        //     }
        // }


        private DataTable _wellsTable;
        public DataTable WellsTable
        {
            get { return _wellsTable; }
            set
            {
                _wellsTable = value;
                RaisePropertyChanged(WellsTablePropertyName);
            }
        }


        private string _selectedFile = string.Empty;
        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    RaisePropertyChanged(SelectedFilePropertyName);
                }
            }
        }

        private int _totalWellCount;
        public int TotalWellCount
        {
            get { return _totalWellCount; }
            set
            {
                if (_totalWellCount != value)
                {
                    _totalWellCount = value;
                    RaisePropertyChanged(TotalWellCountPropertyName);
                }
            }
        }

        private int _totalLowDropletCount;
        public int TotalLowDropletCount
        {
            get { return _totalLowDropletCount; }
            set
            {
                if (_totalLowDropletCount != value)
                {
                    _totalLowDropletCount = value;
                    RaisePropertyChanged(TotalLowDropletCountPropertyName);
                }
            }
        }

        private int _dropletThreshold = DefaultThreshold;
        public int DropletThreshold
        {
            get { return _dropletThreshold; }
            set
            {
                // if (_dropletThreshold != value)
                // {
                _dropletThreshold = value;
                RaisePropertyChanged(DropletThresholdPropertyName);
                // }
            }
        }



        #endregion

        #region Commands

        private RelayCommand _processSelectedFileCommand;
        public RelayCommand ProcessSelectedFileCommand
        {
            get
            {
                if (_processSelectedFileCommand == null)
                {
                    _processSelectedFileCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            _filePath = _fileIOService.OpenDatafile();
                            _jsonDataService.SetFilePath(_filePath);
                            _dropletBin = _jsonDataService.GetDropletBinFromJson();
                            SelectedFile = _filePath;
                            WellsTable = _jsonDataService.GetDataTableFromJson();
                            TotalWellCount = ((WellsTable.Columns.Count - 1) * (WellsTable.Rows.Count));
                            TotalLowDropletCount = GetLowDropletCount(_dropletBin);
                        }
                        catch (Exception ex)
                        {
                            //TODO: Logging
                        }

                    });
                }
                return _processSelectedFileCommand;
            }
        }


        private RelayCommand<int> _updateThresholdCommand;


        public RelayCommand<int> UpdateThresholdCommand
        {
            get
            {
                if (_updateThresholdCommand == null)
                {
                    _updateThresholdCommand = new RelayCommand<int>((threshold) =>
                    {
                        if ((threshold < 0) || (threshold > 500))
                        {
                            DropletThreshold = DefaultThreshold;
                        }
                        else
                        {
                            DropletThreshold = threshold;
                        }
                        if (DropletThreshold == _previousDropletThreshold)
                        {
                            return;
                        }
                        _previousDropletThreshold = DropletThreshold;
                        ForceGridRedrawForLowDropletCounts();
                        TotalLowDropletCount = GetLowDropletCount(_dropletBin);
                    });
                }
                return _updateThresholdCommand;
            }
        }
        #endregion

        #region Ctor

        public MainViewModel(IJsonDataService jsonDataService, IFileIOService fileIoService)
        {
            _jsonDataService = jsonDataService;
            _fileIOService = fileIoService;
        }


        #endregion

        #region Helper Methods    

        private int GetLowDropletCount(DropletBin dropletBin)
        {
            if (dropletBin == null)
            {
                return -1;
            }

            var lowCount = 0;
            foreach (var well in dropletBin.PlateDropletInfo.DropletInfo.Wells)
            {
                if (well.DropletCount < DropletThreshold)
                {
                    lowCount++;
                }
            }
            return lowCount;
        }

        /// <summary>
        /// Refresh DataGrid for background cell color based on droplet value
        /// </summary>
        private void ForceGridRedrawForLowDropletCounts()
        {
            var wellsTable = WellsTable;
            WellsTable = null;
            WellsTable = wellsTable;
        }

        #endregion



    }
}
