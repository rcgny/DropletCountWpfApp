/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:LogExplorer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/
//#define NEED_TO_USE_DESIGN_LOG_DATA_SERVICE_AT_RUNTIME_FOR_TESTING 
using CommonServiceLocator;
using DropletCountWpf.App.Services;
using DropletCountWpfApp.DAL.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;


namespace DropletCountWpf.App.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
               // SimpleIoc.Default.Register<ILogDataService, DesignLogDataService>();
            }
            else
            {
                // Create run time view services and models
#if NEED_TO_USE_DESIGN_JSON_DATA_SERVICE_AT_RUNTIME_FOR_TESTING
                SimpleIoc.Default.Register<IJsonRepository, JsonRepositoryTestData>();
#else
                SimpleIoc.Default.Register<IJsonRepository, JsonRepository>();
#endif
            }

            SimpleIoc.Default.Register<IFileIOService, FileIOService>();
            SimpleIoc.Default.Register<IJsonDataService, JsonDataService>();

            SimpleIoc.Default.Register<MainViewModel>();
             

            }       

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}