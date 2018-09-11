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

namespace WpfApp1 {
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private bool RPC_Active = false;
        public MainWindow() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void SaveSettings(object sender, RoutedEventArgs e) {

        }

        private void ResetToDefaults(object sender, RoutedEventArgs e) {

        }

        private void Start_RPC(object sender, RoutedEventArgs e) {
            if (RPC_Active) {
                Start.Background = Brushes.Transparent;
                Start.Foreground = Brushes.Gray;
                Start.Content = "\uE768";
                RPC_Active = false;
            } else {
                Start.Background = SystemParameters.WindowGlassBrush;
                Start.Foreground = Brushes.White;
                Start.Content = "\uE769";
                RPC_Active = true;
            }
            
        }
    }
}
