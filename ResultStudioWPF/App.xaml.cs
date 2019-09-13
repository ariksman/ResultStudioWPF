using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;
using ResultStudioWPF.Mappers;
using ResultStudioWPF.Views.Windows;

namespace ResultStudioWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
  {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      DispatcherHelper.Initialize();

      PresentationTraceSources.Refresh();
      PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
      PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());

      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
      TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

      var mainWindow = new MainWindow();
      mainWindow.Show();
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }
  }

    public class DebugTraceListener : TraceListener
    {
      public override void Write(string message)
      {
        Debugger.Break();
    }

      public override void WriteLine(string message)
      {
        Debugger.Break();
      }
    }
}
