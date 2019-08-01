using Avalonia;
using Avalonia.Markup.Xaml;

namespace YourRP_Linux
{
    public class App : Application
    {
        public const string DLL = "libdiscord-rpc.so";

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
