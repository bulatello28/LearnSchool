using LearnSchool.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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

namespace LearnSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        private Service _service;
        private bool isEdit = false;
        public ServicePage(Service service)
        {
            InitializeComponent();

            _service = service;

            if (service != null)
            {
                isEdit = true;
            }
            else
            {
                _service = new Service();
            }
        }

        private void BtnGoBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_service.Title) || _service.Cost <= 0 || _service.DurationInSeconds <= 0)
            {
                MessageBox.Show("Введите все поля корректно!");
                return;
            }

            if (!isEdit)
            {
                var res = App.Connection.Service.FirstOrDefault(x => x.Title == _service.Title);
                if (res != null)
                {
                    MessageBox.Show("Такая услуга уже существует");
                    return;
                }
            }

            if (_service.DurationInSeconds * 1.0 / 3600 > 4)
            {
                MessageBox.Show("Услуга с протяженностью более 4 часов не может быть создана!!");
                return;
            }

            App.Connection.Service.AddOrUpdate(_service);
            App.Connection.SaveChanges();


            MessageBox.Show("Услуга успешно сохранена!");

            NavigationService.GoBack();
        }

        private void BtnAddMainImageClick(object sender, RoutedEventArgs e)
        {
            var window = new OpenFileDialog();

            if (window.ShowDialog() != true)
            {
                MessageBox.Show("Изображение не выбрано");
                return;
            }

            var byteArray = File.ReadAllBytes(window.FileName);
            _service.PhotoBytes = byteArray;

            BindingOperations.GetBindingExpressionBase(imageService, Image.SourceProperty).UpdateTarget();

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

            tbID.Visibility = isEdit ? Visibility.Visible : Visibility.Collapsed;
            tblID.Visibility = isEdit ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = _service;
        }
        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                App.Connection.Entry(_service).Reload();
            }
        }


    }
}
