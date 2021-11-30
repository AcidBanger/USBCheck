//<div>Icons made by <a href="https://www.flaticon.com/authors/roundicons" title="Roundicons">Roundicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>

using System;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBCheck
{
    public partial class Hoofdscherm : Form
    {

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
            using StreamWriter file = new("USB Check Log", append: true);
            await file.WriteLineAsync("Device added/removed" + " " + Convert.ToString(WindowsIdentity.GetCurrent().Name) + " " + DateTime.Now.ToString());
        }


    }
}
