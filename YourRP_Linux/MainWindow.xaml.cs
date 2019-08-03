using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Windows.Input;
using YourRPC;

namespace YourRP_Linux
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new WindowIcon(new Uri("res/Discord-RPC.ico", UriKind.Relative).AbsolutePath);
#if DEBUG
            this.AttachDevTools();
#endif
            AddEventHandlers();
           
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
            presence.details = this.Get<TextBox>("DetailsInput").Text;
            presence.state = this.Get<TextBox>("StateInput").Text;
            presence.smallImageKey = this.Get<TextBox>("SmallImgInput").Text;
            presence.smallImageText = this.Get<TextBox>("SmallImgDescInput").Text;
            presence.largeImageKey = this.Get<TextBox>("LargeImgInput").Text;
            presence.largeImageText = this.Get<TextBox>("State").Text;
            DiscordRpc.UpdatePresence(ref presence);
        }

        private void SavePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }

        private bool PresenceISActive = false;
        private void TogglePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (PresenceISActive)
            {
                
            }
            else
            {
                
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
