using API.Models;
using Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для LabPage.xaml
    /// </summary>
    public partial class LabPage : Page
    {
        TimeSpan timeForEnd = new TimeSpan(0, 2, 0);
        bool stopTime = false;

        public LabPage(Staff staff)
        {
            InitializeComponent();

            NameText.Text = staff.FirstName + " " + staff.LastName;
        }

        public async void GetEndTime()
        {
            timeForEnd = timeForEnd.Add(new TimeSpan(0, 0, -1));

            TimeText.Text = timeForEnd.ToString();

            if(stopTime)
            {
                return;
            }

            if (timeForEnd == new TimeSpan(0, 1, 0))
            {
                MessageBox.Show("До окончания сессия остается 15 минут");

                await Task.Delay(1000);
                GetEndTime();
            }
            else if (timeForEnd == new TimeSpan(0, 0, 0))
            {
                (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new Autorization(true);
            }
            else
            {
                await Task.Delay(1000);
                GetEndTime();
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            stopTime = true;

            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new Autorization(false);
        }

        private void EnterCaseClick(object sender, RoutedEventArgs e)
        {
            new CaseEnterWindow().Show();
        }
    }
}
