using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Windows.Input;

namespace YourRP_Linux
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void RefreshPresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }

        private void SavePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }

        private void TogglePresence_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
        }

        private void Testowo(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

       
    }
}
