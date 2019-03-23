using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandExecutor
{
    public class CommandReceived : EventArgs
    {
        public enum CommandTypes { VolumeIncrease, VolumeDecrease, SetVolume, GetVolumeLevel, Mute, IsMuted, Shutdown, CancelShutdown, MoveCursor, SendText };
        public CommandTypes Type { get; private set; }
        public string Message { get; private set; }
        public CommandReceived(CommandTypes type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
