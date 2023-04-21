using LearnSchool.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LearnSchool
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly LanguageSchoolEntities Connection = new LanguageSchoolEntities();

        public static bool IsAdminMode = false;

        public static Visibility IsAdminModeVisible = IsAdminMode ? Visibility.Visible : Visibility.Hidden;

        public static Brush AdditionalBackground = new SolidColorBrush(Color.FromRgb(231, 250, 191));
    }
}
