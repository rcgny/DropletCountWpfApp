using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using DropletCountWpf.App.Services;
using DropletCountWpf.App.Model;

namespace DropletCountWpf.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Data Fields

        private IJsonDataService _jsonDataService;
        private IFileIOService _fileIOService;
        private string _filePath = string.Empty;
        private const string WellsForDataGrid96PropertyName = "WellsForDataGrid96";
        private const string WellsForDataGrid48PropertyName = "WellsForDataGrid48";
        private const string SelectedFilePropertyName = "SelectedFile";
        private const string TotalWellCountPropertyName = "TotalWellCount";
        private const string IsGrid96VisibletPropertyName = "IsGrid96Visible";
        private const string DropletThresholdPropertyName = "DropletThreshold";

        


        #endregion

        #region Properties     


        // rcg: In order to be able to replace the entire collection, in the command handler below, AND trigger the binding, 
        //      had to add a backing variable, since replacing the instance of the actual public LogDataCollection OC property
        //      breaks the original xaml binding...
        private ObservableCollection<WellForDataGrid96> _wellsForDataGrid96 = new ObservableCollection<WellForDataGrid96>();
        public ObservableCollection<WellForDataGrid96> WellsForDataGrid96
       {
            get { return _wellsForDataGrid96; }
            set
            {
                _wellsForDataGrid96 = value;
                RaisePropertyChanged(WellsForDataGrid96PropertyName);
            }
        }

        private ObservableCollection<WellForDataGrid48> _wellsForDataGrid48 = new ObservableCollection<WellForDataGrid48>();
        public ObservableCollection<WellForDataGrid48> WellsForDataGrid48
        {
            get { return _wellsForDataGrid48; }
            set
            {
                _wellsForDataGrid48 = value;
                RaisePropertyChanged(WellsForDataGrid48PropertyName);
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

        private int  _totalWellCount;
        public int  TotalWellCount
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

        private string _dropletThreshold = "100";
        public string DropletThreshold
        {
            get { return _dropletThreshold; }
            set
            {
                if (_dropletThreshold != value)
                {
                    _dropletThreshold = value;
                    RaisePropertyChanged(DropletThresholdPropertyName);
                }
            }
        }


        private bool _isGrid96Visible = true;  // Default to show 96 count grid
        public bool IsGrid96Visible
        {
            get { return _isGrid96Visible; }
            set
            {
                if (_isGrid96Visible != value)
                {
                    _isGrid96Visible = value;
                    RaisePropertyChanged(IsGrid96VisibletPropertyName);
                }
            }
        }

       


        #endregion

        #region Commands

        private RelayCommand _openfileCommand;
        public RelayCommand OpenFileCommand
        {
            get
            {
                if (_openfileCommand == null)
                {
                    _openfileCommand = new RelayCommand(() =>
                    {
                        _filePath = _fileIOService.OpenDatafile();
                        if (!string.IsNullOrEmpty(_filePath))
                        {
                            _jsonDataService.SetFilePath(_filePath);
                            SelectedFile = _filePath;
                            // TODO: Replace this hack!!!
                            if (_filePath.Contains("48") )
                            {
                                IsGrid96Visible = false;
                                WellsForDataGrid48 = _jsonDataService.GetCollection48ForDataGrid();
                                TotalWellCount = WellsForDataGrid48.Count * 6;
                            }
                            else
                            {
                                IsGrid96Visible = true;
                                WellsForDataGrid96 = _jsonDataService.GetCollection96ForDataGrid();
                                TotalWellCount = WellsForDataGrid96.Count * 12;
                            }                           
                        }
                    });
                }
                return _openfileCommand;
            }
        }

        
        private RelayCommand<string> _updateThresholdCommand;
        public RelayCommand<string> UpdateThresholdCommand
        {
            get
            {
                if (_updateThresholdCommand == null)
                {
                    _updateThresholdCommand = new RelayCommand<string>((threshold) =>
                    {
                        DropletThreshold = threshold;
                       
                        if (!string.IsNullOrEmpty(_filePath))
                        {
                            _jsonDataService.SetFilePath(_filePath);
                            SelectedFile = _filePath;
                            // TODO: Replace this hack!!!
                            if (_filePath.Contains("48"))
                            {
                                IsGrid96Visible = false;
                                WellsForDataGrid48 = _jsonDataService.GetCollection48ForDataGrid();
                                TotalWellCount = WellsForDataGrid48.Count * 6;
                            }
                            else
                            {
                                IsGrid96Visible = true;
                                WellsForDataGrid96 = _jsonDataService.GetCollection96ForDataGrid();
                                TotalWellCount = WellsForDataGrid96.Count * 12;
                            }
                        }
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

        #region Methods    

        #endregion



    }
}
