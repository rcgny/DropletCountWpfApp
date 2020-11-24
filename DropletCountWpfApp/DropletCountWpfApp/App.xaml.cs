using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DropletCountWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {   // Open Single file by association support
            Debug.Print("LogExp-Application_Startup: arg count: {0}", e.Args.Length);
            if (e.Args.Length == 1)
            {
                FileInfo file = new FileInfo(e.Args[0]);
                if (file.Exists)
                {
                    Application.Current.Properties["FilePathForSingleFile"] = file.FullName;
                    Debug.Print("LogExp-Application_Startup: filename: {0}", file.FullName);
                }
                else
                {
                    Application.Current.Properties["FilePathForSingleFile"] = null;
                }
            }
            // To trap unhandled exceptions at different levels:
            // 1. AppDomain.CurrentDomain.UnhandledException:   From all threads in the AppDomain.
            // 2. Dispatcher.UnhandledException:  From a single specific UI dispatcher thread.
            // 3. Application.Current.DispatcherUnhandledException:  From the main UI dispatcher thread in your WPF application.
            // 4. TaskScheduler.UnobservedTaskException:  from within each AppDomain that uses a task scheduler for asynchronous operations.

            var currentApplication = System.Windows.Application.Current;
            currentApplication.DispatcherUnhandledException += CurrentApplication_DispatcherUnhandledException;
            var appDomain = AppDomain.CurrentDomain;
            appDomain.UnhandledException += AppDomainOnUnhandledException;

        }

        // UI thread
        private void CurrentApplication_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true; // Allow app to continue.
            var exception = e.Exception;
            var msg = exception.Message + System.Environment.NewLine + exception.StackTrace + System.Environment.NewLine;
            // TODO: Add logging
            // IE File.AppendAllText("LogExplorer.Exception.txt", msg);
        }

        // Any thread
        private void AppDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var exception = unhandledExceptionEventArgs.ExceptionObject as Exception;
            var msg = exception.Message + System.Environment.NewLine + exception.StackTrace + System.Environment.NewLine;
           // TODO: Add logging
           // IE File.AppendAllText("LogExplorer.Exception.txt", msg);
        }       

    }
}
