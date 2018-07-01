using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
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

namespace LinqToRxLabWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
            //.Where(s => s.EventArgs.GetPosition(this.wd).X > 1)
            //.Subscribe(s => {
            //    var x = s.EventArgs.GetPosition(this.wd).X;
            //    var y = s.EventArgs.GetPosition(this.wd).Y;
            //    var myBrush = new LinearGradientBrush();
            //    myBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            //    myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 1 / x));
            //    myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1 / y));
            //    tbStatus.Background = myBrush;
            //}
            //);
            //this.MouseMove += (s, args) =>
            //{
            //    if (args.GetPosition(this.wd).X > 100)
            //        this.tbStatus.Text = "超过了100";
            //    else
            //        this.tbStatus.Text = "没有超过100";
            //};

            var synchContext = new SynchronizationContextScheduler(SynchronizationContext.Current);

            //新的事件流，全部由MouseMove事件构成
            Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")

            //三秒内之内没有新的MouseMove推过来，则往下传送到ObserveOn
            .Throttle(TimeSpan.FromSeconds(3))

            //把线程切到主线程
            .ObserveOn(synchContext)

            //修改控件属性的值，这必须在主线程（拥有控件的线程）中才行
            .Subscribe(s => this.tbStatus.Text = "鼠标3秒钟没动静了");

            //新的事件流，全部由MouseMove事件构成
            Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")

            //没有做任何筛选，所有事件订阅下面的方法
            .Subscribe(s => this.tbStatus.Text = "鼠标动了");
        }
    }
}
