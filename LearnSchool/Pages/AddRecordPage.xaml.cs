using LearnSchool.Components;
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
using System.Xml.Linq;

namespace LearnSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClientRecordPage.xaml
    /// </summary>
    public partial class AddRecordPage : Page
    {
        private Service _service;
        public AddRecordPage(Service service)
        {
            InitializeComponent();

            _service = service;

            this.DataContext = _service;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            cbClients.ItemsSource = App.Connection.Client.ToList();
        }

        private void BtnAddRecordClick(object sender, RoutedEventArgs e)
        {
            if (cbClients.SelectedItem == null)
            {
                MessageBox.Show(messageBoxText: "Укажите клиента!");
                return;
            }

            DateTime parsedTime;
            DateTime.TryParse(tbTime.Text, out parsedTime);

            if (parsedTime == DateTime.MinValue || startDatePicker.SelectedDate == null)
            {
                MessageBox.Show(messageBoxText: "Данные даты или времени не корректны!");
                return;
            }

            var hours = parsedTime.Hour;
            var minutes = parsedTime.Minute;

            ClientService clientService = new ClientService()
            {
                Client = cbClients.SelectedItem as Client,
                Service = _service,
                Comment = tbComment.Text,
                StartTime = (startDatePicker.SelectedDate).Value.AddHours(hours).AddMinutes(minutes)
            };

            App.Connection.ClientService.Add(clientService);
            App.Connection.SaveChanges();

            MessageBox.Show(messageBoxText: "Запись успешно добавлена!");
            NavigationService.GoBack();
        }
    }
}
