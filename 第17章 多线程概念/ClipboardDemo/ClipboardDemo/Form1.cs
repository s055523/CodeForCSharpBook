using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //清空剪贴板
            Clipboard.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("没有数据输入");
            }
            else
            {
                Clipboard.SetDataObject(textBox1.Text);
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject() == null)
            {
                MessageBox.Show("剪贴板上没有数据");
                return;
            }
            var data = Clipboard.GetDataObject();

            //是文本数据
            if (data.GetDataPresent(DataFormats.Text))
            {
                //获得文本数据
                textBox1.Text = data.GetData(DataFormats.Text).ToString();
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("剪贴板上没有数据");
                    return;
                }
            }
            else
            {
                MessageBox.Show("不是文本数据");
            }
        }
    }
}
