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
using System.Timers;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace YourRPC {
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private DiscordRpc.RichPresence presence;
        DiscordRpc.EventHandlers handlers;

        private bool RPC_Active = false;
        private int s = 0;
        //make config object
        config Config = new config();
        public MainWindow() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loadSettings();
            if (!IsWindows10()) {
                //MainWindow.SetTintOpacity(this, 1);
                //MainWindow.SetNoiseOpacity(this, 0);
            }
            SourceChord.FluentWPF.SystemTheme.ThemeChanged += this.SystemTheme_ThemeChanged;
            ChFontColor(null, null);


        }

        private void SystemTheme_ThemeChanged(object sender, EventArgs e)
        {
            ChFontColor(null, null);
        }

        private static string SettingsDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".YourRP");
        private static string SettingsPath = System.IO.Path.Combine(SettingsDirPath, "config.json");

        private void loadSettings() {
            //load to Config
            Config.clientID = Properties.Settings.Default.ClientID;
            Config.details = Properties.Settings.Default.Details;
            Config.state = Properties.Settings.Default.State;
            Config.sm_img = Properties.Settings.Default.sm_img;
            Config.sm_img_txt = Properties.Settings.Default.sm_img_txt;
            Config.lg_img = Properties.Settings.Default.lg_img;
            Config.lg_img_txt = Properties.Settings.Default.lg_img_txt;

            //set fields in window
            ClientID.Text = Config.clientID;
            Details.Text = Config.details;
            State.Text = Config.state;
            Small_Image.Text = Config.sm_img;
            Small_Image_Desc.Text = Config.sm_img_txt;
            Large_Image.Text = Config.lg_img;
            Large_Image_Desc.Text = Config.lg_img_txt;
        }

        private void SaveSettings(object sender, RoutedEventArgs e) {
            //save to config
            Config.clientID = ClientID.Text;
            Config.details = Details.Text;
            Config.state = State.Text;
            Config.sm_img = Small_Image.Text;
            Config.sm_img_txt = Small_Image_Desc.Text;
            Config.lg_img = Large_Image.Text;
            Config.lg_img_txt = Large_Image_Desc.Text;

            //save to file

            if (!Directory.Exists(SettingsDirPath))
            {
                Directory.CreateDirectory(SettingsDirPath);
            }
            if (!File.Exists(SettingsPath))
            {
                File.Create(SettingsPath);
            }

            RefreshPresenceContents();
            FileStream file = File.OpenWrite(SettingsPath);
            file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(presence, Formatting.Indented)), 0, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(presence, Formatting.Indented)).Length);

            YourRPC.Properties.Settings.Default.ClientID = Config.clientID;
            YourRPC.Properties.Settings.Default.Details = Config.details;
            YourRPC.Properties.Settings.Default.State = Config.state;
            YourRPC.Properties.Settings.Default.sm_img = Config.sm_img;
            YourRPC.Properties.Settings.Default.sm_img_txt = Config.sm_img_txt;
            YourRPC.Properties.Settings.Default.lg_img = Config.lg_img;
            YourRPC.Properties.Settings.Default.lg_img_txt = Config.lg_img_txt;
            YourRPC.Properties.Settings.Default.Save();
        }

        private void RefreshPresenceContents()
        {
            presence.details = Details.Text;
            presence.state = State.Text;
            presence.smallImageKey = Small_Image.Text;
            presence.smallImageText = Small_Image_Desc.Text;
            presence.largeImageKey = Large_Image.Text;
            presence.largeImageText = Large_Image_Desc.Text;
        }

        private void Start_RPC(object sender, RoutedEventArgs e) {
            if (RPC_Active) {
                Start.Background = System.Windows.Media.Brushes.Transparent;
                Start.Foreground = (SolidColorBrush)FindResource("DynamicFG");
                Start.Content = "\uE768";
                RPC_Active = false;
                Shutdown();
            } else {
                long a;
                if(!long.TryParse(ClientID.Text, out a)) {
                    MessageBox.Show("")
                    return;
                }

                Start.Background = SystemParameters.WindowGlassBrush;
                Start.Foreground = System.Windows.Media.Brushes.White;
                Start.Content = "\uE71A";
                RPC_Active = true;
                Initialize(ClientID.Text);
            }

            
        }

        private void Shutdown() {
            DiscordRpc.Shutdown();

            this.SetStatusBarMessage("Shuted down.");
        }

        /// <summary>
		/// Initialize the RPC.
		/// </summary>
		/// <param name="clientId"></param>
		private void Initialize(string clientId) {
            handlers = new DiscordRpc.EventHandlers();

            //handlers.readyCallback = ReadyCallback;
            //handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;

            DiscordRpc.Initialize(clientId, ref handlers, true, null);
            Update(null, null);
            
        }

        private void ErrorCallback(int errorCode, string message) {
            
        }

        private void SetStatusBarMessage(string message) {
            //this.Label_Status.Content = message;
        }

        private void Update(object sender, RoutedEventArgs e) {
            RefreshPresenceContents();
            DiscordRpc.UpdatePresence(ref presence);
        }

        private void OpenSettings(object sender, RoutedEventArgs e) {
            SettingsWindow settingsWin = new SettingsWindow();
            settingsWin.ShowDialog();
        }

        public void ChFontColor(object sender, RoutedEventArgs e) {
            if (IsDarkmode() && IsWindows10()) {
                this.Resources["DynamicFG"] = new SolidColorBrush(Colors.White);
            } else {
                this.Resources["DynamicFG"] = new SolidColorBrush(Colors.Black);
            }
        }

        public static bool IsDarkmode() {
            RegistryKey rkSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
            if (rkSubKey == null) {
                rkSubKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
                if (rkSubKey == null) return false;
            }
            string LightMode = rkSubKey.GetValue("AppsUseLightTheme").ToString();

            switch (LightMode) {
                case "1":
                    return false;
                case "0":
                    return true;
                default:
                    return false;
            }
        }

        

        public static bool IsWindows10() {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
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


