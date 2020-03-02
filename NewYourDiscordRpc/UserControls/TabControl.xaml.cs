using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewYourDiscordRpc.UserControls
{
    public class TabControl : UserControl
    {
        private StackPanel Tabs;
        private Grid TabContent;

        private List<CTabItem> Items;
        public TabControl()
        {
            InitializeComponent();
        }
        
        
        public TabControl(List<CTabItem> Items)
        {
            InitializeComponent();

            this.Items = Items;
            Tabs = this.Get<StackPanel>("Tabs");
            TabContent = this.Get<Grid>("TabContent");
            foreach (CTabItem i in Items)
            {
                Button bt= new Button {Content = i.Header};
                bt.Click += (sender, args) =>
                {
                    foreach (Button b in Tabs.Children)
                    {
                        b.Tag = "";
                    }

                    ((Button) sender).Tag = "Selected";
                    CTabItem ct = Items.FirstOrDefault(x => x.Header == ((string) ((Button) sender).Content));
                    TabContent.Children.Clear();
                    TabContent.Children.Add(ct.Content);
                };
                Tabs.Children.Add(bt);
            }

            ((Button) Tabs.Children[0]).Tag = "Selected";
            TabContent.Children.Clear();
            TabContent.Children.Add(Items[0].Content);



        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}