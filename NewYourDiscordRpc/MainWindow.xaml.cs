using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewYourDiscordRpc
{
    public class MainWindow : Window
    {
        #region UI declarations

        #region Toolbar

        private Button StartButton;
        private Button StopButton;
        private Button RefreshButton;
        private Button SaveButton;

        #endregion

        #endregion
        public MainWindow()
        {
            InitializeComponent();

            InitUI();


        }

        private void InitUI()
        {
            Toolbar();



            void Toolbar()
            {
                StartButton = this.Get<Button>("StartButton");
                StopButton = this.Get<Button>("StopButton");
                RefreshButton = this.Get<Button>("RefreshButton");
            }

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}