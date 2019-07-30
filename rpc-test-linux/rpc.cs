using System;
using System.Collections.Generic;
using System.Text;

namespace rpc_test_linux
{
    class rpc
    {

        public DiscordRpc.RichPresence presence;

        DiscordRpc.EventHandlers handlers;

        public void Start_RPC()
        {

            long a = 488870967217487872;

            Initialize(a.ToString());



        }

        public void Initialize(string clientId)
        {
            handlers = new DiscordRpc.EventHandlers();

            //handlers.readyCallback = ReadyCallback;
            //handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;

            DiscordRpc.Initialize(clientId, ref handlers, true, null);
            Update();

        }

        public void Update()
        {
            presence.details = "Details";
            presence.state = "State";
            presence.smallImageKey = "";
            presence.smallImageText = "";
            presence.largeImageKey = "";
            presence.largeImageText = "";
            DiscordRpc.UpdatePresence(ref presence);
        }

        public void ErrorCallback(int errorCode, string message)
        {

        }

        public void Shutdown()
        {
            DiscordRpc.Shutdown();

        }
    }
}
