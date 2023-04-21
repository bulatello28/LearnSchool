using LearnSchool.Components;
using LearnSchool.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public List<Service> SourceData { get; set; }

        private Predicate<Service> _filterQuery = x => true;

        private Func<Service, object> _sortQuery = x => x.ID;
        public ServicesPage()
        {
            InitializeComponent();
            if (App.IsAdminMode == false)
            {
                btnLeave.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnLeave.Visibility = Visibility.Visible;
            }
        }
        private void TbSearchNameTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchServicesByName();
        }

        private void SearchServicesByName()
        {
            if (lvServices == null)
            {
                return;
            }

            lvServices.ItemsSource = SourceData.Where(x => x.Title.ToLower().Contains(tbSearchName.Text.ToLower())).ToList();
        }

        private void UpdateData()
        {
            if (lvServices == null)
            {
                return;
            }

            SourceData = App.Connection.Service.ToList()
                 .Where(
                 x => _filterQuery(x) && x.IsMarkedForDeletion != true)
                 .OrderBy(x => _sortQuery(x))
                 .ToList();

            SearchServicesByName();

        }

        private void CbSortPriceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortPrice();
        }

        private void SortPrice()
        {
            if (lvServices == null)
            {
                return;
            }

            ComboBoxItem item = (ComboBoxItem)cbSortPrice.SelectedItem;
            string selectedSortText = item.Content.ToString();

            switch (selectedSortText)
            {
                case "По возрастанию":
                    _sortQuery = x => x.CostWithDiscount;
                    break;

                case "По убыванию":
                    _sortQuery = x => -x.CostWithDiscount;
                    break;

                default:
                    _sortQuery = x => x.ID;
                    break;
            }

            UpdateData();
        }

        private void FiltrateDiscount()
        {
            if (lvServices == null)
            {
                return;
            }

            ComboBoxItem item = (ComboBoxItem)cbFilterDiscount.SelectedItem;
            string selectedSortText = item.Content.ToString();

            switch (selectedSortText)
            {
                case "от 0 до 10%":
                    _filterQuery = x => x.Discount >= 0 && x.Discount < 10;
                    break;

                case "от 10 до 20%":
                    _filterQuery = x => x.Discount >= 10 && x.Discount < 20;
                    break;

                case "от 20 до 40%":
                    _filterQuery = x => x.Discount >= 20 && x.Discount < 40;
                    break;

                case "от 40 до 70%":
                    _filterQuery = x => x.Discount >= 40 && x.Discount < 70;
                    break;

                case "от 70 до 100%":
                    _filterQuery = x => x.Discount >= 70 && x.Discount < 100;
                    break;

                default:
                    _filterQuery = x => true;
                    break;
            }

            UpdateData();
        }

        private void CbFilterDiscountSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrateDiscount();
        }

        private void AdminModeBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminAuthPage());
        }

        private void ServicesPageLoaded(object sender, RoutedEventArgs e)
        {
            UpdateData();

            lvServices.ItemsSource = SourceData;

            adminModeBtn.Visibility = App.IsAdminMode ? Visibility.Collapsed : Visibility.Visible;
            btnAddNewService.Visibility = App.IsAdminMode ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnEditServiceClick(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            var service = App.Connection.Service.FirstOrDefault(x => x.ID == tag);
            NavigationService.Navigate(new ServicePage(service));
        }

        private void BtnDeleteServiceClick(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            var service = App.Connection.Service.FirstOrDefault(x => x.ID == tag);

            var clientServices = App.Connection.ClientService.ToList().Where(x => x.ServiceID == tag);

            if (clientServices.Any())
            {
                MessageBox.Show("Какие-то записи ещё оформлены на эту услугу!");
                return;
            }

            service.IsMarkedForDeletion = true;
            App.Connection.Service.AddOrUpdate(service);
            App.Connection.SaveChanges();

            UpdateData();

            MessageBox.Show("Услуга успешно удалена!");

        }

        private void BtnAddNewServiceClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage(null));
        }

        private void LvServicesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!App.IsAdminMode)
            {
                return;
            }

            NavigationService.Navigate(new AddRecordPage(lvServices.SelectedItem as Service));
        }

        private void BtnShowClientRecordsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RecordsPage());
        }

        private void btnLeave_Click(object sender, RoutedEventArgs e)
        {
            App.IsAdminMode = false;
        }
    }
}
