using System;

namespace rpc_test_linux
{
    class Program
    {
        public const string DLL = "libdiscord-rpc";



        static void Main(string[] args)
        {
            rpc RPC = new rpc();
            RPC.Start_RPC();
            System.Threading.Thread.Sleep(10000);
            RPC.Shutdown();

            Console.WriteLine("Hello World!");
        }

        
    }
}
