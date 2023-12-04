using API.Models;
using FinalAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
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

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для LabResPage.xaml
    /// </summary>
    public partial class LabResPage : Page
    {
        List<int> askingOrderService = new List<int>();
        bool cycleTrue = false;

        public LabResPage(Staff staff)
        {
            InitializeComponent();

            NameText.Text = staff.FirstName + " " + staff.LastName;

            GetInfo();
        }

        public async void GetCycle()
        {
            if(!(askingOrderService.Count == 0))
            {
                cycleTrue = true;

                var tempAskingOrderService = new List<int>(askingOrderService);

                foreach (int id in tempAskingOrderService)
                {
                    var client = new HttpClient();
                    var response = await client.PutAsync("http://localhost:5283/api/OrderServices/progress/" + id.ToString(), null);
                }

                GetUtilizerService(UtilizerComboBox.SelectedValue as string, true);
            }
            else
            {
                cycleTrue = false;
            }
        }

        public async void GetUtilizerService(string name, bool isCycle)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/Utilizers/name=" + name);

            var json = await responce.Content.ReadFromJsonAsync<List<UtilizerService>>();

            var deviationResult = json.FirstOrDefault(x => x.StatusName == "Требуется повторная утилизация");

            if (deviationResult != null && askingOrderService.Contains(deviationResult.Id))
            {
                askingOrderService.Remove(deviationResult.Id);

                var messageAnswer = MessageBox.Show("Выполненная услуга с кодом " + deviationResult.Code + " отличается от среднего результата в пять раз (" + deviationResult.Result + "). Хотите ли вы ее одобрить?", "", MessageBoxButton.YesNo);

                if (messageAnswer == MessageBoxResult.Yes)
                {
                    responce = await client.PutAsync("http://localhost:5283/api/OrderServices/approve/" + deviationResult.Id, null);

                    GetUtilizerService(UtilizerComboBox.SelectedValue as string, false);
                }
            }

            foreach (var item in json.Where(x => askingOrderService.Contains(x.Id)))
            {
                if(item.StatusName != "Начато")
                {
                    askingOrderService.Remove(item.Id);
                }
            }

            var startedService = json.FirstOrDefault(x => x.StatusName == "Начато");

            if (startedService != null && !askingOrderService.Contains(startedService.Id) && isCycle == false)
            {
                var messageAnswer = MessageBox.Show("Начатая ранее услуга не была завершена. Хотите продолжить ее выполнение сейчас?", "", MessageBoxButton.YesNo);

                if (messageAnswer == MessageBoxResult.Yes)
                {
                    askingOrderService.Add(json.FirstOrDefault(x => x.StatusName == "Начато").Id);

                    GetUtilizerService(UtilizerComboBox.SelectedValue as string, !cycleTrue);
                }
            }

            if (UtilizerComboBox.SelectedValue as string == name)
            {
                UtilizerDataGrid.ItemsSource = null;
                UtilizerDataGrid.ItemsSource = json;
            }

            if (isCycle)
            {
                GetCycle();
            }
        }

        public async void GetInfo()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/Utilizers");

            var json = await responce.Content.ReadFromJsonAsync<List<Utilizer>>();

            UtilizerComboBox.ItemsSource = json.ConvertAll(x => x.Name);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new Autorization(false);
        }

        private void UtilizerChanged(object sender, SelectionChangedEventArgs e)
        {
            GetUtilizerService(UtilizerComboBox.SelectedValue as string, false);
        }

        private async void SendServiceClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as UtilizerService;

            if (item.StatusName == "Не начато" || item.StatusName == "Требуется повторная утилизация")
            {
                var client = new HttpClient();
                var responce = await client.PutAsync("http://localhost:5283/api/OrderServices/utilization/" + item.Id.ToString(), null);

                if(responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Утилизатор уже находится в работе");
                }
                else
                {
                    askingOrderService.Add(item.Id);

                    GetUtilizerService(UtilizerComboBox.SelectedValue as string, !cycleTrue);

                    MessageBox.Show("Услуга отправлена на утилизатор");
                }
            }
            else if(item.StatusName == "Завершено" || (item.StatusName == "Начато" && askingOrderService.Contains(item.Id)))
            {
                MessageBox.Show("Услуга уже начата или завершена");
            }
            else
            {
                askingOrderService.Add(item.Id);

                GetUtilizerService(UtilizerComboBox.SelectedValue as string, !cycleTrue);
            }
        }

        private async void ApproveServiceClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as UtilizerService;

            var client = new HttpClient();
            var responce = await client.PutAsync("http://localhost:5283/api/OrderServices/approve/" + item.Id, null);

            GetUtilizerService(UtilizerComboBox.SelectedValue as string, false);
        }
    }
}
