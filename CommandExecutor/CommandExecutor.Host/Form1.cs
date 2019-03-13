using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandExecutor.Host
{
    public partial class Form1 : Form
    {
        private ServiceHost host;
        private CommandExecutorService commandExecutorService;
        public Form1()
        {
            InitializeComponent();
            commandExecutorService = new CommandExecutorService();
            commandExecutorService.MessageRecieved += CommandExecutorService_MessageRecieved;
            WindowState = FormWindowState.Minimized;
            
        }

        private void CommandExecutorService_MessageRecieved(object sender, CommandReceived commandReceived)
        {
            richTextBox1.Text += DateTime.Now + ": " + commandReceived.Type + " " + commandReceived.Message + Environment.NewLine;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            host.Close();
        }

        private void ToTray()
        {
            Hide();
            notifyIcon.Visible = true;            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ToTray();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            host = new ServiceHost(commandExecutorService);
            host.Open();
        }
    }
}
