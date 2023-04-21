using LearnSchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LearnSchool.Components
{
    public partial class Service
    {
        public Visibility IsHaveDiscount => Discount > 0 ? Visibility.Visible : Visibility.Collapsed;
        public decimal CostWithDiscount => Discount > 0 ? Cost - Cost * (decimal)Discount / 100 : Cost;
        public string DurationInMinutes => DurationInSeconds == 0 ? "" : $"{DurationInSeconds / 60} минут";
        public string DiscountDisplay => Discount == null || Discount == 0 ? "" : $"* скидка {Discount}%";
        public string ImagePath => $@"/{MainImagePath}";
        public Visibility AdminVisibility => App.IsAdminMode ? Visibility.Visible : Visibility.Collapsed;
        public Brush ServiceBackgroundColor => Discount > 0 ? Brushes.LightGreen : Brushes.Transparent;
    }
}
