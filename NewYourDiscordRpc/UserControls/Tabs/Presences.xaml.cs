using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewYourDiscordRpc.UserControls.Tabs
{
    public class Presences : UserControl
    {
        public Presences()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}