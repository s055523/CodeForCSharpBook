using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMessageLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //得到窗口句柄
            IntPtr myIntPtr = FindWindow(null, "Form1");

            //通过句柄向Windows发出消息，设置窗口位置
            SetWindowPos(myIntPtr, (IntPtr)(-1), 0, 0, 0, 0, 0x0040 | 0x0001); 
            
            //设置鼠标位置，使其落在第二个按钮上
            SetCursorPos(80, 120);

            //模拟鼠标按下操作
            mouse_event(0x0002, 0, 0, 0, 0);

            //模拟鼠标放开操作
            mouse_event(0x0004, 0, 0, 0, 0); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi");
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
    }
}
