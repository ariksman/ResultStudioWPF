using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ResultStudioWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
  {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      var mainWindow = new MainWindow();
      mainWindow.Show();
    }
  }
}
