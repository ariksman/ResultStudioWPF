using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using ResultStudioWPF.Messages;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Views
{
    /// <summary>
    /// Interaction logic for DataChart2View.xaml
    /// </summary>
    public partial class DataChartView : UserControl
    {
        public DataChartView()
        {
            InitializeComponent();
            // Messages
            AppMessages.PlotRefresh.Register(this, RefreshPlot);
        }

        private void RefreshPlot(bool b)
        {
            this.XaxisPlot.InvalidatePlot();
            this.YaxisPlot.InvalidatePlot();
            this.ZaxisPlot.InvalidatePlot();
        }
    }
}
