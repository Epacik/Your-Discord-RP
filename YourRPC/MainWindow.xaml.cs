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
using DiscordRPC;

namespace YourRPC {
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        static private DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();
        DiscordRpc.EventHandlers handlers;

        Config cfg;

        private bool RPC_Active = false;
        private int s = 0;
        //make config object
        public MainWindow() {
            InitializeComponent();
            cfg = new Config();
            loadSettings();
            if (!IsWindows10()) {
                //MainWindow.SetTintOpacity(this, 1);
                //MainWindow.SetNoiseOpacity(this, 0);
            }

            if (!Directory.Exists(SettingsDirPath))
            {
                Directory.CreateDirectory(SettingsDirPath);
            }
            if (!File.Exists(SettingsPath))
            {
                File.Create(SettingsPath);
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
            if (Directory.Exists(SettingsDirPath) && File.Exists(SettingsPath))
            {
                try
                {
                    StreamReader c = File.OpenText(SettingsPath);
                    cfg = JsonConvert.DeserializeObject<Config>(c.ReadToEnd());

                    ClientID.Text = cfg.clientID;
                    Details.Text = cfg.presence.details;
                    State.Text = cfg.presence.state;
                    Small_Image.Text = cfg.presence.smallImageKey;
                    Small_Image_Desc.Text = cfg.presence.smallImageText;
                    Large_Image.Text = cfg.presence.largeImageKey;
                    Large_Image_Desc.Text = cfg.presence.largeImageText;
                    presence = cfg.presence;
                }
                catch (Exception e)
                {

                }

            }

        }

        private void SaveSettings(object sender, RoutedEventArgs e) {
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
            FileStream file;
            while (true)
            {
                try
                {
                    file = File.OpenWrite(SettingsPath);
                    break;
                }
                catch(Exception ex)
                {

                }
            }

            Config cfg = new Config
            {
                clientID = ClientID.Text,
                presence = presence,
            };
            
            file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cfg, Formatting.Indented)), 0, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cfg, Formatting.Indented)).Length);

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
                    MessageBox.Show("");
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

    class Config {
        public string clientID { get; set; }
        public YourRPC.DiscordRpc.RichPresence presence { get; set; }
        
    }
    

   

    
}


