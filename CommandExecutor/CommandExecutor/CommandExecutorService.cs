using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandExecutor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class CommandExecutorService : ICommandExecutorService
    {
        public delegate void MessageReceivedHandler(object sender, CommandReceived commandReceived);
        public event MessageReceivedHandler MessageRecieved;
        private CoreAudioDevice defaultPlaybackDevice;

        public CommandExecutorService()
        {
            
        }

        public int GetVolumeLevel()
        {
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.GetVolumeLevel, defaultPlaybackDevice.Volume.ToString()));
            return (int)defaultPlaybackDevice.Volume;
        }

        public void Mute()
        {
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Mute(!defaultPlaybackDevice.IsMuted);
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.Mute, defaultPlaybackDevice.IsMuted.ToString()));
        }

        public bool IsMuted()
        {
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.IsMuted, defaultPlaybackDevice.IsMuted.ToString()));
            return defaultPlaybackDevice.IsMuted;
        }

        public void VolumeDecrease()
        {
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume -= 10;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.VolumeDecrease, $"new volume is {defaultPlaybackDevice.Volume}"));
        }

        public void VolumeIncrease()
        {
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume += 10;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.VolumeIncrease, $"new volume is {defaultPlaybackDevice.Volume}"));
        }

        public void ShutDown()
        {
            Process.Start("shutdown", "/s");
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.Shutdown, ""));
        }

        public void CancelShutDown()
        {
            Process.Start("shutdown", "/a");
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.CancelShutdown, ""));
        }

        public void SetVolume(int level)
        {
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = level;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.SetVolume, $"new volume is {defaultPlaybackDevice.Volume}"));
        }

        public void MoveCursor(int dx, int dy)
        {
            Cursor.Position = new Point(Cursor.Position.X + dx, Cursor.Position.Y + dy);
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.MoveCursor, $"move cursor dx = {dx} dy = {dy}"));
        }
    }
}
