using System.Windows;
using System.ServiceModel;
using ServiceModel;

namespace WpfHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServiceHost service = new ServiceHost(typeof(CalanderService));
            service.Open();
        }
    }
}
