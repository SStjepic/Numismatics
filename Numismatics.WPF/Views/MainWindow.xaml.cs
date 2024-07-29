using Numismatics.WPF.ViewModels.Home;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Numismatics.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HomePageViewModel HomeViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            HomeViewModel = new HomePageViewModel();
            DataContext = this;
        }
    }
}