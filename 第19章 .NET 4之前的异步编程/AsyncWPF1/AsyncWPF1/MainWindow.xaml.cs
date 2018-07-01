using System.ComponentModel;
using System.Windows;

namespace AsyncWPF1
{
    public partial class MainWindow
    {
        //声明BackgroundWorker
        private readonly BackgroundWorker bw;

        public MainWindow()
        {
            InitializeComponent();

            //实例化BackgroundWorker并设置
            bw = new BackgroundWorker
            {
                //设置可以通告进度
                WorkerReportsProgress = true,

                //设置可以中途取消
                WorkerSupportsCancellation = true
            };

            //为子线程挂接任务
            bw.DoWork += DoWork;

            //进度报告事件
            bw.ProgressChanged += UpdateProgress;

            //任务完成事件
            bw.RunWorkerCompleted += CompletedWork;

            btnCancel.IsEnabled = false;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var number = (int)e.Argument;
            var count = 1;

            for (var i = 2; i < number; i++)
            {
                //已经取消了
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                if (number % i == 0)
                {
                    e.Result = false;
                    return;
                }

                //进度模拟
                if (i >= number / 100 * count)
                {
                    bw.ReportProgress(count);
                    count++;
                }
            }
            e.Result = true;
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //由于DoWork在子线程上运行，所以不能在那里访问tbNumber.Text
            int number;

            //参数检查
            if (!int.TryParse(tbNumber.Text, out number))
            {
                tbStatus.Text = string.Format("输入必须为绝对值不超过{0}的整数", int.MaxValue);
                return;
            }

            if (number <= 0)
            {
                tbStatus.Text = "输入必须大于0";
                return;
            }
            if (number == 1)
            {
                tbStatus.Text = "1不是质数也不是合数";
                return;
            }

            btnCancel.IsEnabled = true;
            btnTest.IsEnabled = false;

            //调用RunWorkerAsync，它隐式创建子线程运行DoWork，并传入参数
            bw.RunWorkerAsync(number);
        }

        //进度报告
        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            pb.Value = e.ProgressPercentage;
        }

        //任务完成（相当于回调函数）
        void CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("任务出现异常！详情为：" + e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("任务取消！");
            }
            else
            {
                MessageBox.Show("任务成功完成！结果为" + e.Result);
            }

            pb.Value = 100;
            btnCancel.IsEnabled = false;
            btnTest.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            bw.CancelAsync();
        }
    }
}
