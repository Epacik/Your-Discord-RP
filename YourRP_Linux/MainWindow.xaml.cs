using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using Avalonia.Media;
using Newtonsoft.Json;
using YourRPC;

namespace YourRP_Linux
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new WindowIcon("res/Discord-RPC.ico");
#if DEBUG
            this.AttachDevTools();
#endif
            AddEventHandlers();
            LoadConfig();
           
        }

        private void AddEventHandlers()
        {
            this.Get<Button>("ToggleButton").Click += TogglePresence_Click;
            this.Get<Button>("SaveButton").Click += SavePresence_Click;
            this.Get<Button>("RefreshButton").Click += RefreshPresence_Click;
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
            if (!File.Exists(SettingsPath))
            {
                File.Create(SettingsPath);
            }

            RefreshPresenceContents();
            FileStream file = File.OpenWrite(SettingsPath);
            file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(presence, Formatting.Indented)));
        }

        public void LoadConfig()
        {
            if (Directory.Exists(SettingsDirPath) && File.Exists(SettingsPath))
            {
                StreamReader cfg = File.OpenText(SettingsPath);
                presence = JsonConvert.DeserializeObject<DiscordRpc.RichPresence>(cfg.ReadToEnd());
                
                this.Get<TextBox>("DetailsInput").Text = presence.details;
                this.Get<TextBox>("StateInput").Text = presence.state;
                this.Get<TextBox>("SmallImgInput").Text = presence.smallImageKey;
                this.Get<TextBox>("SmallImgDescInput").Text = presence.smallImageText;
                this.Get<TextBox>("LargeImgInput").Text = presence.largeImageKey;
                this.Get<TextBox>("LargeImgDescInput").Text = presence.largeImageText;
            }
        }

        private bool PresenceISActive = false;
        private void TogglePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (PresenceISActive)
            {
                DiscordRpc.Shutdown();
                this.Get<Button>("ToggleButton").BorderBrush = Brushes.LightGray;
                this.Get<Button>("ToggleButton").BorderThickness = new Thickness(1, 1,1,1);
                this.Get<Button>("ToggleButton").Padding = new Thickness(20,0,0,0);
                this.Get<Button>("ToggleButton").Content = "Start";
                PresenceISActive = false;
            }
            else
            {
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


        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

       
    }
}
