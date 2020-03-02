using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

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
        private Button MenuButton;

        #endregion

        private ScrollViewer MenuScrollViewer;


        #endregion
        public MainWindow()
        {
            InitializeComponent();

            InitUI();


        }

        private DispatcherTimer menuTimer;

        private void InitUI()
        {
            Toolbar();
            MenuScrollViewer = this.Get<ScrollViewer>("MenuScrollViewer");


            void Toolbar()
            {
                //Get Buttons on toolbar
                StartButton = this.Get<Button>("StartButton");
                StopButton = this.Get<Button>("StopButton");
                RefreshButton = this.Get<Button>("RefreshButton");
                SaveButton = this.Get<Button>("SaveButton");
                MenuButton = this.Get<Button>("MenuButton");

                //Apply events to buttons
                StartButton.Click += StartButton_Click;
                StopButton.Click += StopButton_Click;
                RefreshButton.Click += RefreshButton_Click;
                SaveButton.Click += SaveButton_Click;
                MenuButton.Click += (sender, args) =>
                {
                    if (MenuScrollViewer.Classes.Contains("Show") || MenuScrollViewer.Classes.Contains("Showing"))
                    {
                        Console.WriteLine(MenuScrollViewer.Classes.ToString());
                        MenuScrollViewer.Classes.Remove("Show");
                        MenuScrollViewer.Classes.Remove("Showing");
                        
                        Console.WriteLine(MenuScrollViewer.Classes.ToString());
                        MenuScrollViewer.Classes.Add("Hiding");
                        menuTimer = new DispatcherTimer(
                            new TimeSpan(0,0,0,0,110),
                            DispatcherPriority.Send,
                            (o, a) =>
                            {
                                Console.WriteLine(MenuScrollViewer.Classes.ToString());
                                MenuScrollViewer.Classes.Remove("Hiding");
                                MenuScrollViewer.Classes.Add("Hide"); 
                                //MenuScrollViewer.Width = 0;
                                Console.WriteLine(MenuScrollViewer.Classes.ToString());
                            });
                    }
                    else
                    {
                        Console.WriteLine(MenuScrollViewer.Classes.ToString());
                        MenuScrollViewer.Classes.Remove("Hide");
                        MenuScrollViewer.Classes.Remove("Hiding");
                        
                        Console.WriteLine(MenuScrollViewer.Classes.ToString());
                        MenuScrollViewer.Classes.Add("Showing");
                        menuTimer = new DispatcherTimer(
                            new TimeSpan(0,0,0,0,110),
                            DispatcherPriority.Send,
                            (o, a) =>
                            {
                                Console.WriteLine(MenuScrollViewer.Classes.ToString());
                                MenuScrollViewer.Classes.Remove("Showing");
                                MenuScrollViewer.Classes.Add("Show");
                                //MenuScrollViewer.Width = 150;
                                Console.WriteLine(MenuScrollViewer.Classes.ToString());
                            });
                    }
                }; 


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