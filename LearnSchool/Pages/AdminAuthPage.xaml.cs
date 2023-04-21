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

namespace LearnSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminAuthPage.xaml
    /// </summary>
    public partial class AdminAuthPage : Page
    {
        public AdminAuthPage()
        {
            InitializeComponent();
        }

        private void btnGoAdminMode_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdminCode.Text == "0000")
            {
                App.IsAdminMode = true;
            }

            NavigationService.GoBack();
        }
    }
}
