using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// no bloc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var lablContent = await GetAsync();//ritorna a ui thread intanto libera ui
            MyLabel.Content = lablContent;//ritorna qui quando GetAsync è finito e threa ui esegue codfice

            /*
             * 
             * That awaiter is responsible for hooking up the callback 
             * (often referred to as the “continuation”) that will call back into the state machine
             * when the awaited object completes, 
             * and it does so using whatever context/scheduler it captured at the time the callback was registered.
             */

            //HttpClient s_httpClient = new HttpClient();
            //string text = await s_httpClient.GetStringAsync("http://example.com/currenttime");
            //MyLabel.Content = text;
        }

        /// <summary>
        /// bloc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var lablContent = GetAsyncAvoidDeadLock().Result;//blocca ui thread in attesa che finisce
            MyLabel.Content = lablContent;//solo ui thread puo fare modifiche al ui ma è bloccato da result
        }


        private async Task<string> GetAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });

            return "ciao";
        }

        /// <summary>
        /// avoid deadlock
        /// </summary>
        private async Task<string> GetAsyncAvoidDeadLock()
        {
            /*
             * ConfigureAwait(false)
             * is used to avoid forcing the callback to be invoked on the original context or scheduler. 
             */
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            }).ConfigureAwait(false);

            return "ciao";
        }

        /// <summary>
        /// avoid deadlock bloc
        /// </summary>
        private void GetAsyncAvoidDeadLock1()
        {
            HttpClient s_httpClient = new HttpClient();
            s_httpClient.GetStringAsync("http://example.com/currenttime").ContinueWith(downloadTask =>
                {
                    MyLabel.Content = downloadTask.Result;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        /// <summary>
        /// avoid deadlock bloc
        /// </summary>
        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient s_httpClient = new HttpClient();
            SynchronizationContext sc = SynchronizationContext.Current;
            s_httpClient.GetStringAsync("http://example.com/currenttime").ContinueWith(downloadTask =>
            {
                sc.Post(delegate
                {
                    MyLabel.Content = downloadTask.Result;
                }, null);
            });
        }

        /// <summary>
        /// deadlock
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAsyncDeadLock()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });

            return "ciao";
        }
    }
}
