//<div>Icons made by <a href="https://www.flaticon.com/authors/roundicons" title="Roundicons">Roundicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBCheck
{
    public partial class Hoofdscherm : Form
    {

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);



        private const int WM_DEVICECHANGE = 0x219;
        private const int DBT_DEVNODES_CHANGED = 0X0007;

        public Hoofdscherm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    switch ((int)m.WParam)
                    {
                        case DBT_DEVNODES_CHANGED:
                            listBox1.Items.Add("USB added/removed");
                            ExampleAsync();
                            break;
                    }
                    break;
            }
        }
        public static async Task ExampleAsync()
        {
            using StreamWriter file = new("USB_Check_Log.txt", append: true);
            await file.WriteLineAsync("Device added/removed" + " " + Convert.ToString(WindowsIdentity.GetCurrent().Name) + " " + DateTime.Now.ToString());

            int n = 20;
            for (int i = 1; i < n; i++)
            {
                SOUNDUP();
                Console.Beep(800, 500);
            }           
        }

        public static int SOUNDUP()
        {
            int v = 500;
            for (int i = 1; i < v; i++)
                keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
            return 1;
        }
    }
}
