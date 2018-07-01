using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetTotalByteByFolder(string folderName, CancellationTokenSource cts)
        {
            long sum = 0;
            var sw = new Stopwatch();
            sw.Start();

            //使用任务，并传入取消令牌
            Task.Run(() => GetTotalByteParallel(folderName, sum, cts), cts.Token)

            //使用任务的接续处理异常，因为任务是单任务，没有子任务，所以使用接续不会阻塞
            .ContinueWith(t =>
            {
                sw.Stop();
                var information = string.Empty;
                Dispatcher.Invoke(() =>
                {
                    tbStatus.Text += sw.ElapsedMilliseconds + "毫秒\n";
                });
                //用户取消了
                if (t.IsCanceled || cts.IsCancellationRequested)
                {
                    Dispatcher.Invoke(() =>
                    {
                        information += "任务取消！\n";
                        information += string.Format("{0}统计了{1}字节。\n", tbPath.Text, 1);
                    });
                }
                //任务出现异常
                else if (t.IsFaulted)
                {
                    foreach (var aeInnerException in t.Exception.InnerExceptions)
                    {
                        information += string.Format("捕捉到一个异常：{0}\n", aeInnerException.Message);
                    }
                }
                //成功执行
                else
                {
                    //使用Dispatcher切换回UI线程，才能操作控件（下同）
                    //如果直接操作，则会抛出异常，而且因为该异常也是任务的异常，外部并没有等待它，所以它消失，不会传播
                    Dispatcher.Invoke(() =>
                    {
                        information = string.Format("{0}的总大小为{1}字节。\n", tbPath.Text, t.Result);
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    tbStatus.Text += information;
                    btnStart.IsEnabled = true;
                    btnCancel.IsEnabled = false;
                });
            });
        }

        //递归的获得目录下所有物件的长度，包括子目录
        private long GetTotalByteParallel(string folderName, long sum, CancellationTokenSource cts)
        {
            while (true)
            {
                //每统计一个对象都检查用户是否已经请求了取消
                cts.Token.ThrowIfCancellationRequested();

                var directory = new DirectoryInfo(folderName);
                if (!directory.Exists)
                {
                    throw new ArgumentException("目录不存在！");
                }

                //文件
                Parallel.ForEach(directory.GetFiles(), (file, state) =>
                {
                    if (cts.IsCancellationRequested)
                    {
                        state.Break();
                    }
                    Interlocked.Add(ref sum, file.Length);
                });

                //目录（递归）
                Parallel.ForEach(directory.GetDirectories(), (d, state) =>
                {
                    if (cts.IsCancellationRequested)
                    {
                        state.Break();
                    }
                    Interlocked.Add(ref sum, GetTotalByteParallel(d.FullName, 0, cts));
                });
                return sum;
            }
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnCancel.IsEnabled = true;
            tbStatus.Text = "";
            _cts = new CancellationTokenSource();
            GetTotalByteByFolder(tbPath.Text, _cts);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
        }
    }
}
