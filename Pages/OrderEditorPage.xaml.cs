using API.Models;
using Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderEditorPage.xaml
    /// </summary>
    public partial class OrderEditorPage : Page
    {
        public class CaseInfo
        {
            public List<Client> Clients { get; set; }
            public List<Company> Companies { get; set; }
            public List<Service> Services { get; set; }
            public int NextId { get; set; }
            public int NextNumber { get; set; }
        }

        List<Service> SelectedService = new List<Service>();
        Client SelectedClient;
        CaseInfo allInfo;
        int number;

        public OrderEditorPage(int num)
        {
            InitializeComponent();

            number = num;
            GetInfo();
        }

        public async void GetInfo()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/GetCaseInfo");
            allInfo = responce.Content.ReadFromJsonAsync<CaseInfo>().Result;

            ClientGrid.ItemsSource = allInfo.Clients;
            AllServiceGrid.ItemsSource = allInfo.Services;
        }

        private void ClientSelected(object sender, MouseButtonEventArgs e)
        {
            var selectedClient = (sender as DataGrid).SelectedItem as Client;

            if (selectedClient != null)
            {
                SelectedClient = selectedClient;

                SelectedClientText.Text = SelectedClient.FullName;
            }
        }

        private void ClientSearchChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchClient.Text != "")
            {
                var clientBySearch = allInfo.Clients.Where(x =>
                x.FullName.Contains(SearchClient.Text) ||
                x.BirthDate.ToString().Contains(SearchClient.Text) ||
                x.PasSeries.Contains(SearchClient.Text) ||
                x.PasNumber.Contains(SearchClient.Text) ||
                x.Phone.Contains(SearchClient.Text) ||
                x.Mail.Contains(SearchClient.Text) ||
                FuzzySearch(x.FullName, SearchClient.Text)
                ).ToList();

                ClientGrid.ItemsSource = null;
                ClientGrid.ItemsSource = clientBySearch;
            }
            else
            {
                ClientGrid.ItemsSource = null;
                ClientGrid.ItemsSource = allInfo.Clients;
            }
        }

        public bool FuzzySearch(string FirstString, string SecondString)
        {
            int i = 0;

            i += Math.Abs(FirstString.Length - SecondString.Length);

            if(i > 3)
            {
                return false;
            }

            for (int j = 0; j < Math.Min(FirstString.Length, SecondString.Length); j++)
            {
                if (FirstString[j] != SecondString[j])
                {
                    i++;
                }

                if(i > 3)
                {
                    return false;
                }
            }

            return true;
        }

        private void ServiceSearchChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchService.Text != "")
            {
                var serviceBySearch = allInfo.Services.Where(x =>
                x.Name.Contains(SearchService.Text) ||
                x.Price.ToString().Contains(SearchService.Text) ||
                FuzzySearch(x.Name, SearchService.Text)
                ).ToList();

                AllServiceGrid.ItemsSource = null;
                AllServiceGrid.ItemsSource = serviceBySearch;
            }
            else
            {
                AllServiceGrid.ItemsSource = null;
                AllServiceGrid.ItemsSource = allInfo.Services;
            }
        }

        private void ServiceSelected(object sender, MouseButtonEventArgs e)
        {
            var selected = (sender as DataGrid).SelectedItem as Service;

            if (selected != null)
            {
                SelectedService.Add(selected);
                SelectedServiceGrid.ItemsSource = null;
                SelectedServiceGrid.ItemsSource = SelectedService;
            }

            PriceText.Text = SelectedService.ConvertAll(x => x.Price).Sum().ToString("F2");
        }

        private void ServiceDelete(object sender, MouseButtonEventArgs e)
        {
            var selected = (sender as DataGrid).SelectedItem as Service;

            if (selected != null)
            {
                SelectedService.Remove(selected);
                SelectedServiceGrid.ItemsSource = null;
                SelectedServiceGrid.ItemsSource = SelectedService;
            }

            PriceText.Text = SelectedService.ConvertAll(x => x.Price).Sum().ToString("F2");
        }

        private void OpenUserCreator(object sender, RoutedEventArgs e)
        {
            new UserCreatorWindow(allInfo.NextNumber).Show();
        }

        private async void OrderSaveClick(object sender, RoutedEventArgs e)
        {
            //try
            //{
                var newOrder = new Order()
                {
                    Id = allInfo.NextId,
                    CreateDate = DateTime.Now,
                    Client = SelectedClient.Id,
                    Number = number,
                    Status = 1
                };

                var client = new HttpClient();
                var json = JsonSerializer.Serialize(newOrder);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync("http://localhost:5080/api/PostOrder", jsonContent);
            
                json = JsonSerializer.Serialize(SelectedService.ConvertAll(x => x.Code).ToList());
                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                responce = await client.PostAsync("http://localhost:5080/api/PostOrderServices?orderId=" + allInfo.NextId, jsonContent);

                MessageBox.Show("Заказ успешно сохранен");
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Проверьте введенные данные");
            //}
            
        }
    }
}
