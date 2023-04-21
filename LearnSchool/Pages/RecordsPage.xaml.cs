using LearnSchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace LearnSchool.Pages
{
    /// <summary>
    /// Interaction logic for ClientsRecordsPage.xaml
    /// </summary>
    public partial class RecordsPage : Page
    {
        DispatcherTimer _dispatcherTimer;
        public RecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lvRecords.ItemsSource = App.Connection.ClientService.ToList().Where(x => x.StartTime >= DateTime.Now && x.StartTime < DateTime.Now.AddDays(2));
            StartTimer();
        }

        private void StartTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            _dispatcherTimer.Start();
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            lvRecords.ItemsSource = App.Connection.ClientService.ToList().Where(x => x.StartTime >= DateTime.Now && x.StartTime < DateTime.Now.AddDays(2));
        }
    }
}
