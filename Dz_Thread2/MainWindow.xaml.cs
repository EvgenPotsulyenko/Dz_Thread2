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

namespace Dz_Thread2
{
    public partial class MainWindow : Window
    {
        public Thread th1;
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(down.Text == "")
            {
                down.Text = "2";
            }
            int down1 = Convert.ToInt32(down.Text);
            int up1 = Convert.ToInt32(up.Text);
            var th = new Thread(() => create(down1,up1));
            th.Start();
        }
        public void create(int down1,int up1)
        {                     
            for (; down1 < up1; down1++)
            {
                Dispatcher.Invoke(new ThreadStart(delegate { listbox1.Items.Add(down1); }));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            th1 = new Thread(create_fib);
            th1.Start();
        }
        public void create_fib()
        {
            int a1 = 0, a2 = 0, z = 0;
            Dispatcher.Invoke(new ThreadStart(delegate {fib.Items.Add(1); }));
            Dispatcher.Invoke(new ThreadStart(delegate {fib.Items.Add(1); }));
            while (z < 1000)
            {
                a1 = Convert.ToInt32(fib.Items[fib.Items.Count - 1]);
                a2 = Convert.ToInt32(fib.Items[fib.Items.Count - 2]);
                z = a1 + a2;
                Dispatcher.Invoke(new ThreadStart(delegate { fib.Items.Add(z); }));              
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            if (token.IsCancellationRequested)  // проверяем наличие сигнала отмены задачи
            {
                MessageBox.Show("Операция прервана");
                return;     //  выходим из метода и тем самым завершаем задачу
            }
        }
    }
}
