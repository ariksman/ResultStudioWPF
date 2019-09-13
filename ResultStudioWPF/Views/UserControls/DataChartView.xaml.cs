using System.Windows.Controls;
using ResultStudioWPF.ViewModels.Messages;

namespace ResultStudioWPF.Views.UserControls
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
