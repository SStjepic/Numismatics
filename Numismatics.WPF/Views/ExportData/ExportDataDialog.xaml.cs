using Numismatics.WPF.ViewModels.ExportDataViewModel;
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
using System.Windows.Shapes;

namespace Numismatics.WPF.Views.ExportData
{
    /// <summary>
    /// Interaction logic for ExportDataDialog.xaml
    /// </summary>
    public partial class ExportDataDialog : Window
    {
        public ExportDataViewModel ExportDataViewModel { get; set; }
        public ExportDataDialog()
        {
            InitializeComponent();

            ExportDataViewModel = new ExportDataViewModel();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
