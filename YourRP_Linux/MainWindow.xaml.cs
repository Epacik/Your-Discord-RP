using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Avalonia.Media;
using Newtonsoft.Json;
using MessageBox.Avalonia;

namespace YourRP_Linux
{

    
    public class MainWindow : Window
    {
        
        #region Window Elements Definitions

        #region Buttons

        private Button ToggleButton;
        private Button RefreshButton;
        private Button SaveButton;

        
    

        #endregion
    
        private SolidColorBrush MainBgColor;
        private SolidColorBrush MenuBgColor;
        private SolidColorBrush FontColor;


        #endregion
        
        
        
        private static string GnomeSettingsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @".config/gtk-3.0/settings.ini");
        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new WindowIcon("res/Discord-RPC.ico");
#if DEBUG
            this.AttachDevTools();
#endif
            AddEventHandlers();
            
            LoadConfig();
            SetTheme();
            

        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            #region Buttons

            ToggleButton = this.Get<Button>("ToggleButton");
            SaveButton = this.Get<Button>("SaveButton");
            RefreshButton = this.Get<Button>("RefreshButton");

            #endregion

            MainBgColor = this.Get<SolidColorBrush>("MainBgColor");
            MenuBgColor = this.Get<SolidColorBrush>("MenuBgColor");
            FontColor = this.Get<SolidColorBrush>("FontColor");

        }

        private void SetTheme()
        {
            //MainBgColor.Color = Color.FromRgb(255, 128, 0);
            if (File.Exists(GnomeSettingsPath))
            {
                string GtkConfig = File.ReadAllText(GnomeSettingsPath);
                List<string> ConfigLines = GtkConfig.Split(Environment.NewLine).ToList();
                string darkmodeSetting = ConfigLines.FirstOrDefault(l => l.StartsWith("gtk-application-prefer-dark-theme"));
                if (darkmodeSetting != null &&
                    (darkmodeSetting.Contains("1") || darkmodeSetting.ToLower().Contains("true")))
                {
                    //Set darker colors of ui and light font colors
                    MainBgColor.Color = Color.FromRgb(0,0,0);
                    MenuBgColor.Color = Color.FromRgb(40, 40, 40);
                    FontColor.Color = Colors.White;
                }
            }
            
        }

        private void AddEventHandlers()
        {
            ToggleButton.Click += TogglePresence_Click;
            SaveButton.Click += SavePresence_Click;
            RefreshButton.Click += RefreshPresence_Click;
        }

        
        private DiscordRpc.RichPresence presence;
        DiscordRpc.EventHandlers handlers;
        
        private void RefreshPresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            RefreshPresenceContents();
            DiscordRpc.UpdatePresence(ref presence);
        }

        private void RefreshPresenceContents()
        {
            presence.details = this.Get<TextBox>("DetailsInput").Text;
            presence.state = this.Get<TextBox>("StateInput").Text;
            presence.smallImageKey = this.Get<TextBox>("SmallImgInput").Text;
            presence.smallImageText = this.Get<TextBox>("SmallImgDescInput").Text;
            presence.largeImageKey = this.Get<TextBox>("LargeImgInput").Text;
            presence.largeImageText = this.Get<TextBox>("LargeImgDescInput").Text;
        }

        private static string SettingsDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".YourRP");
        private static string SettingsPath = Path.Combine(SettingsDirPath, "config.json");

        
        
        private void SavePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
            if (!Directory.Exists(SettingsDirPath))
            {
                Directory.CreateDirectory(SettingsDirPath);
            }
//            if (!File.Exists(SettingsPath))
//            {
//                File.Create(SettingsPath);
//            }

            RefreshPresenceContents();
            FileStream file = File.OpenWrite(SettingsPath);
            Config cfg = new Config
            {
                clientID = this.Get<TextBox>("ClientIDInput").Text,
                presence = presence,
            };

            file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cfg, Formatting.Indented)));
        }

        public void LoadConfig()
        {
            if (Directory.Exists(SettingsDirPath) && File.Exists(SettingsPath))
            {
                try
                {
                    StreamReader cfg = File.OpenText(SettingsPath);
                    Config c = JsonConvert.DeserializeObject<Config>(cfg.ReadToEnd());

                    presence = c.presence;

                    this.Get<TextBox>("ClientIDInput").Text = c.clientID;
                    this.Get<TextBox>("DetailsInput").Text = presence.details;
                    this.Get<TextBox>("StateInput").Text = presence.state;
                    this.Get<TextBox>("SmallImgInput").Text = presence.smallImageKey;
                    this.Get<TextBox>("SmallImgDescInput").Text = presence.smallImageText;
                    this.Get<TextBox>("LargeImgInput").Text = presence.largeImageKey;
                    this.Get<TextBox>("LargeImgDescInput").Text = presence.largeImageText;
                }
                catch (Exception e)
                {

                }
            }
        }

        private bool PresenceISActive = false;
        private void TogglePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (PresenceISActive)
            {
                DiscordRpc.Shutdown();
                this.Get<Button>("ToggleButton").BorderBrush = Brushes.LightGray;
                this.Get<Button>("ToggleButton").BorderThickness = new Thickness(0, 0,0,0);
                this.Get<Button>("ToggleButton").Padding = new Thickness(20,0,0,0);
                this.Get<Button>("ToggleButton").Content = "Start";
                PresenceISActive = false;
            }
            else
            {
                long a = 0;
                
                if (!long.TryParse(this.Get<TextBox>("ClientIDInput").Text, out a))
                {
                    MessageBoxManager.Instance.Show("Invalid ID", "The Client ID you provided isn't valid,\nplease check it and try again");
                    return;
                }
                InitializePresence(this.Get<TextBox>("ClientIDInput").Text);
                
                this.Get<Button>("ToggleButton").BorderBrush = Brushes.Green;
                this.Get<Button>("ToggleButton").BorderThickness = new Thickness(7, 1,1,1);
                this.Get<Button>("ToggleButton").Padding = new Thickness(14,0,0,0);
                this.Get<Button>("ToggleButton").Content = "Stop";
                RefreshPresence_Click(null, null);
                PresenceISActive = true;
                
            }
        }

        private void InitializePresence(string ClientID)
        {
            handlers = new DiscordRpc.EventHandlers();

            handlers.errorCallback += (code, message) => { };
            handlers.disconnectedCallback += (code, message) => { };

            DiscordRpc.Initialize(ClientID, ref handlers, true, null);
            
        }


        class Config
        {
            public string clientID { get; set; }
            public YourRP_Linux.DiscordRpc.RichPresence presence { get; set; }
        }

    }
}
