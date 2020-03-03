using System;
using System.Collections.Generic;
using System.Net;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Dis = Discord;
using Discord = Discord.Discord;

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

        private Grid View;

        private ScrollViewer MenuScrollViewer;
        
        private Grid DummyGrid1;

        private ScrollViewer DummyScrollViewer;


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

            DummyGrid1 = this.Get<Grid>("DummyGrid1");
            DummyScrollViewer = this.Get<ScrollViewer>("DummyScrollViewer");


            void Toolbar()
            {
                //Get Buttons on toolbar
                StartButton = this.Get<Button>("StartButton");
                StopButton = this.Get<Button>("StopButton");
                RefreshButton = this.Get<Button>("RefreshButton");
                SaveButton = this.Get<Button>("SaveButton");
                MenuButton = this.Get<Button>("MenuButton");

                View = this.Get<Grid>("View");

                //Apply events to buttons
                StartButton.Click += StartButton_Click;
                StopButton.Click += StopButton_Click;
                RefreshButton.Click += RefreshButton_Click;
                SaveButton.Click += SaveButton_Click;
                MenuButton.Click += (sender, args) =>
                {
                    if (DummyScrollViewer.Width >= 150 || Double.IsNaN(DummyScrollViewer.Width))
                    {
                        DummyScrollViewer.Width = 0;
                        DummyScrollViewer.Margin = new Thickness(-160,0,0,0);
                        DummyGrid1.Width = this.Width;
                    }
                    else
                    {
                        DummyScrollViewer.Width = 150;
                        DummyScrollViewer.Margin = new Thickness(0,0,0,0);
                        DummyGrid1.Width = this.Width - 150;
                    }
                }; 


                void StartButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
                {
                    StartButton.IsVisible = false;
                    StopButton.IsVisible = true;
                    
                    Dis.Discord dis = new Dis.Discord(488870967217487872, (UInt64)Dis.CreateFlags.Default);
                    Data.Discord = dis;
                    
                    dis.SetLogHook(Dis.LogLevel.Debug, (level, message) =>
                    {
                        Console.WriteLine("Log[{0}] {1}", level, message);
                    });
                    
                    var applicationManager = dis.GetApplicationManager();
                    // Get the current locale. This can be used to determine what text or audio the user wants.
                    Console.WriteLine($"Current Locale: {applicationManager.GetCurrentLocale()}");
                    // Get the current branch. For example alpha or beta.
                    Console.WriteLine($"Current Branch: {applicationManager.GetCurrentBranch()}");
                    
                    var activityManager = dis.GetActivityManager();
                    
                    UpdateActivity(discord, lobby);
                    
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