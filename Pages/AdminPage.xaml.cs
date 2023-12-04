using API.Models;
using Desktop.Windows;
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

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(Staff staff)
        {
            InitializeComponent();

            NameText.Text = staff.FirstName + " " + staff.LastName;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new Autorization(false);
        }

        private void OpenHistoryClick(object sender, RoutedEventArgs e)
        {
            new LoginHistoryWindow().Show();
        }
    }
}
