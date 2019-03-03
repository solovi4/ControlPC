using CommandExecutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 1) return;
            var channel = new ChannelFactory<ServiceReference1.ICommandExecutorService>("WSHttpBinding_ICommandExecutorService");
            var executor = channel.CreateChannel();
            var command = args[0];
            switch(command)
            {
                case "VolumeIncrease": executor.VolumeIncrease(); break;
                case "VolumeDecrease": executor.VolumeDecrease(); break;
                case "Mute":           executor.Mute();           break;
                case "ShutDown":       executor.ShutDown();       break;
                case "CancelShutDown": executor.CancelShutDown(); break;
            }
            channel.Close();
        }
    }
}
