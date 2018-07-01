using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncWinform1
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource cts;
        private delegate bool IsPrimeSlowDelegate(int number, CancellationToken token);
        public Form1()
        {
            InitializeComponent();

            cts = new CancellationTokenSource();
            btnCancel.Enabled = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //UI线程可以直接更新控件
            btnTest.Enabled = false;
            btnCancel.Enabled = true;

            var del = new IsPrimeSlowDelegate(IsPrimeSlow);
            var callback = new AsyncCallback(CallBack);

            int number;
            if (!int.TryParse(tbNumber.Text, out number))
            {
                UpdateControls(string.Format("输入必须为绝对值不超过{0}的整数", int.MaxValue));
            }
            else
            {
                //开始一个异步任务
                del.BeginInvoke(number, cts.Token, callback, null);
            }
        }

        private static bool IsPrimeSlow(int number, CancellationToken token)
        {
            if (number <= 0) throw new ArgumentException("输入必须大于0");
            if (number == 1) throw new ArgumentException("1不是质数也不是合数");

            for (var i = 2; i < number; i++)
            {
                token.ThrowIfCancellationRequested();
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //回调函数
        private void CallBack(IAsyncResult iar)
        {
            //强行转换为AsyncResult类型
            var ar = (AsyncResult)iar;

            //AsyncDelegate的输入已经在主线程中定义好了，现在只需要调用EndInvoke，而调用EndInvoke需要对应类型的委托的一个实例，故新建一个
            var ba = (IsPrimeSlowDelegate)ar.AsyncDelegate;

            try
            {
                //获得结果，这里不会阻塞
                var ret = ba.EndInvoke(iar);
                var retStr = ret ? "为质数" : "为合数";

                //使用控件的BeginInvoke方法
                tbStatus.BeginInvoke(new UpdateStatusTextBoxDelegate(UpdateControls), "任务成功完成！结果" + retStr);
            }
            catch (OperationCanceledException)
            {
                //捕捉到异常
                tbStatus.BeginInvoke(new UpdateStatusTextBoxDelegate(UpdateControls), "任务取消！");
            }
            catch (ArgumentException ae)
            {
                tbStatus.BeginInvoke(new UpdateStatusTextBoxDelegate(UpdateControls), ae.Message);
            }
        }

        //更新控件的异步方法的委托
        private delegate void UpdateStatusTextBoxDelegate(string message);

        //更新控件的异步方法，包括将Message显示在文本框中，并且更新按钮的状态
        private void UpdateControls(string message)
        {
            tbStatus.Text = message;
            btnTest.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }

}
