﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace YourRPC {
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application {
        // 32bit Discord RPC DLL
        public const string DLL = "discord-rpc-w32";

        public App() : base() {
            if (!System.IO.File.Exists(DLL + ".dll")) {
                MessageBox.Show(
                    "Missing " + DLL + ".dll\n\n" +
                    "Grab it from the release on GitHub or from the DiscordRpcDemo/lib/ folder in the repo then put it alongside DiscordRpcDemo.exe.\n\n" +
                    "https://github.com/nostrenz/cshap-discord-rpc-demo"
                );

                this.Shutdown();
            }
        }
    }
}
