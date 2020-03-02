using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewYourDiscordRpc.UserControls.Tabs
{
    public class Connection : UserControl
    {
        public Connection()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}