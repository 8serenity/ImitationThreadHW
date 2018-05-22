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

namespace ThreadImitationHW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    


    public partial class MainWindow : Window
    {
        public Task TaskInUI { get; set; }
        public CancellationTokenSource MyCancellationTokenSource { get; set; }
        public CancellationToken MyCancellationToken { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartClicked(object sender, RoutedEventArgs e)
        {
            if (TaskInUI != null && TaskInUI.Status == TaskStatus.Running)
            {
                return;
            }

            MyCancellationTokenSource = new CancellationTokenSource();
            MyCancellationToken = MyCancellationTokenSource.Token;
            TaskInUI = new Task(LongOperation);
            textInfo.Text = "Thread started";
            TaskInUI.Start();
        }

        private void StopClicked(object sender, RoutedEventArgs e)
        {
            if (TaskInUI != null)
            {
                MyCancellationTokenSource.Cancel();
                textInfo.Text = "Thread stopped";
            }
        }

        private void LongOperation()
        {
            for (int i = 0; i < 10000; i++)
            {
                if (MyCancellationToken.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(3000);
            }
        }
    }
}
