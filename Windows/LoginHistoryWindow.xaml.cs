using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginHistory.xaml
    /// </summary>
    public partial class LoginHistoryWindow : Window
    {
        List<LoginHistory> history = new List<LoginHistory>();

        public LoginHistoryWindow()
        {
            InitializeComponent();

            GetHistory();
        }

        public async void GetHistory()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/GetHistory");
            history = responce.Content.ReadFromJsonAsync<List<LoginHistory>>().Result;

            HistoryGrid.ItemsSource = history;
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            Temp();
        }

        private void SearchChanged(object sender, SelectionChangedEventArgs e)
        {
            Temp();
        }
         private void Temp()
        {
            var nowHistory = history;

            if (LoginSearch.Text != "")
            {
                nowHistory = history.Where(x => x.Login.Contains(LoginSearch.Text)).ToList();
            }

            if (StartDate.SelectedDate != null)
            {
                nowHistory = nowHistory.Where(x => x.Date.Date >= StartDate.SelectedDate).ToList();
            }

            if (EndDate.SelectedDate != null)
            {
                nowHistory = nowHistory.Where(x => x.Date.Date <= EndDate.SelectedDate).ToList();
            }

            HistoryGrid.ItemsSource = nowHistory;
        }
        
    }
}
