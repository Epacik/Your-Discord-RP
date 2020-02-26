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
                //Get Buttons on toolbar
                StartButton = this.Get<Button>("StartButton");
                StopButton = this.Get<Button>("StopButton");
                RefreshButton = this.Get<Button>("RefreshButton");
                SaveButton = this.Get<Button>("SaveButton");

                //Apply events to buttons
                StartButton.Click += StartButton_Click;
                StopButton.Click += StopButton_Click;
                RefreshButton.Click += RefreshButton_Click;
                SaveButton.Click += SaveButton_Click;


                void StartButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
                {
                    StartButton.IsVisible = false;
                    StopButton.IsVisible = true;
                }

                void StopButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
                {
                    StartButton.IsVisible = true;
                    StopButton.IsVisible = false;
                }

                void RefreshButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
                {
                }

                void SaveButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
                {
                }
            }

        }

        

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}