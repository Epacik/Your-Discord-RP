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
using YourRPC;

namespace WpfApp1 {
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private bool RPC_Active = false;
        config Config = new config();
        public MainWindow() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Config.clientID = YourRPC.Properties.Settings.Default.ClientID;
            Config.details = YourRPC.Properties.Settings.Default.Details;
            Config.state = YourRPC.Properties.Settings.Default.State;
            Config.sm_img = YourRPC.Properties.Settings.Default.sm_img;
            Config.sm_img_txt = YourRPC.Properties.Settings.Default.sm_img_txt;
            Config.lg_img = YourRPC.Properties.Settings.Default.lg_img;
            Config.lg_img_txt = YourRPC.Properties.Settings.Default.lg_img_txt;
        }

        private void loadSettings() {

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

    class config {
        public string clientID;
        public string details;
        public string state;
        public string sm_img;
        public string sm_img_txt;
        public string lg_img;
        public string lg_img_txt;
    }

    class defaultConfig {
        public const string ClientID = "488870967217487872";
        public const string Details = "Make your own";
        public const string State = "custom Rich Presence";
        public const string Sm_img = "rich_presence";
        public const string Sm_img_txt = "Your Discord RP";
        public const string Lg_img = "discord-logo-white";
        public const string Lg_img_txt = "Discord";
    }
}
