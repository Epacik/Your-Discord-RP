using Avalonia.Controls;

namespace NewYourDiscordRpc
{
    public class CTabItem
    {
        public CTabItem(string header, Control Content)
        {
            Header = header;
            this.Content = Content;
        }
    
        public string Header { get; set; }
        public Control Content { get; set; }
    }
}