using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }

        public int GetVolumeLevel()
        {
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.GetVolumeLevel, defaultPlaybackDevice.Volume.ToString()));
            return (int)defaultPlaybackDevice.Volume;
        }

        public void Mute()
        {
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
            defaultPlaybackDevice.Volume -= 10;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.VolumeDecrease, $"new volume is {defaultPlaybackDevice.Volume}"));
        }

        public void VolumeIncrease()
        {
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
            defaultPlaybackDevice.Volume = level;
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.SetVolume, $"new volume is {defaultPlaybackDevice.Volume}"));
        }

        public void MoveCursor(int dx, int dy)
        {
            Cursor.Position = new Point(Cursor.Position.X + dx, Cursor.Position.Y + dy);
            MessageRecieved?.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.MoveCursor, $"move cursor dx = {dx} dy = {dy}"));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public void MouseLeftClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public void MouseRightClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        public void SendText(string text)
        {
            SendKeys.SendWait(text);
            MessageRecieved.Invoke(this, new CommandReceived(CommandReceived.CommandTypes.SendText, text));
        }
    }
}
