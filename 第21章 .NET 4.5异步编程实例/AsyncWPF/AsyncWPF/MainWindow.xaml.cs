using System;
using System.Collections.Generic;
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

namespace AsyncWPF
{
    public partial class MainWindow
    {
        //支持取消
        private CancellationTokenSource cts;

        readonly Progress<double> progress = new Progress<double>();

        public MainWindow()
        {
            InitializeComponent();

            //为进度条设置值
            progress.ProgressChanged += (s, args) =>
            {
                pb.Value = args;
            };
        }

        private async void btnTest_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            int number;

            //参数检查
            if (!int.TryParse(tbNumber.Text, out number))
            {
                tbStatus.Text = string.Format("输入必须为绝对值不超过{0}的整数", int.MaxValue);
                return;
            }

            btnCancel.IsEnabled = true;
            btnTest.IsEnabled = false;
            pb.Value = 0;

            try
            {
                tbStatus.Text = await Task.Run(() => IsPrimeLow(number, progress, cts.Token) ? "true" : "false");
            }
            catch (ArgumentException ae)
            {
                tbStatus.Text = ae.Message;
            }
            catch (OperationCanceledException)
            {
                tbStatus.Text = "任务取消！";
            }
            finally
            {
                btnCancel.IsEnabled = false;
                btnTest.IsEnabled = true;
                pb.Value = 100;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private async void btnTest2_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();

            try
            {
                btnTest2.IsEnabled = false;
                btnCancel.IsEnabled = true;
                pb.Value = 0;

                var task1 = Task.Run(() => IsPrimeLow(10, progress, cts.Token));
                var task2 = Task.Run(() => IsPrimeLow(100, progress, cts.Token));
                var task3 = Task.Run(() => IsPrimeLow(1073676287, progress, cts.Token));

                //等待三个任务全部完成
                var results = await Task.WhenAll(task1, task2, task3);

                MessageBox.Show("三个任务全部完成");

                //结果:False,False,True
                tbStatus.Text = "结果:" + string.Join(",", results);
            }
            catch (ArgumentException ae)
            {
                tbStatus.Text = ae.Message;
            }
            catch (OperationCanceledException)
            {
                tbStatus.Text = "任务取消！";
            }
            finally
            {
                btnCancel.IsEnabled = false;
                btnTest2.IsEnabled = true;
                pb.Value = 100;
            }
        }

        private async void btnTest3_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();

            try
            {
                btnTest2.IsEnabled = false;
                btnCancel.IsEnabled = true;
                pb.Value = 0;

                var task1 = Task.Run(() => IsPrimeLow(10, progress, cts.Token));
                var task2 = Task.Run(() => IsPrimeLow(100, progress, cts.Token));
                var task3 = Task.Run(() => IsPrimeLow(1073676287, progress, cts.Token));

                //等待任务完成
                var t = await Task.WhenAny(task1, task2, task3);

                MessageBox.Show("第一个任务已经完成");

                //结果:False,False,True
                tbStatus.Text = "结果:" + t.Result;
            }
            catch (ArgumentException ae)
            {
                tbStatus.Text = ae.Message;
            }
            catch (OperationCanceledException)
            {
                tbStatus.Text = "任务取消！";
            }
            finally
            {
                btnCancel.IsEnabled = false;
                btnTest2.IsEnabled = true;
                pb.Value = 100;
            }
        }

        private async void btnTest4_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();

            try
            {
                btnTest2.IsEnabled = false;
                btnCancel.IsEnabled = true;
                pb.Value = 0;

                var task1 = Task.Run(() => IsPrimeLow(10, progress, cts.Token));
                var task2 = Task.Run(() => IsPrimeLow(100, progress, cts.Token));
                var task3 = Task.Run(() => IsPrimeLow(1073676287, progress, cts.Token));

                //定义一个任务的集合
                var tasks = new[] { task1, task2, task3 };

                //任意一个任务完成时就输出结果
                var allTasks = tasks.Select(async t =>
                {
                    var result = await t;
                    tbStatus.Text += result + ",";
                }).ToArray();

                //等待任务集合的完成
                await Task.WhenAll(allTasks);
            }
            catch (ArgumentException ae)
            {
                tbStatus.Text = ae.Message;
            }
            catch (OperationCanceledException)
            {
                tbStatus.Text = "任务取消！";
            }
            finally
            {
                btnCancel.IsEnabled = false;
                btnTest2.IsEnabled = true;
                pb.Value = 100;
            }
        }

        //异步任务
        private static bool IsPrimeLow(int number, IProgress<double> progress, CancellationToken ct)
        {
            if (number <= 0) throw new ArgumentException("输入必须大于0");
            if (number == 1) throw new ArgumentException("1不是质数也不是合数");

            var count = 1;
            for (int i = 2; i < number; i++)
            {
                ct.ThrowIfCancellationRequested();
                if (number % i == 0)
                {
                    progress.Report(100);
                    return false;
                }

                //进度模拟
                if (i >= number / 100 * count)
                {
                    progress.Report(count);
                    count++;
                }
            }
            return true;
        }
    }

}
