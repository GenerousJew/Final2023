using API.Models;
using Desktop.Classes;
using Desktop.Pages;
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
    /// Логика взаимодействия для UserCreatorWindow.xaml
    /// </summary>
    public partial class UserCreatorWindow : Window
    {
        int number;
        public UserCreatorWindow(int num)
        {
            InitializeComponent();

            number = num;
            GetCompanies();
        }

        public async void GetCompanies()
        {
            var CompanyList = new List<Company>();

            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/GetCompanies");
            CompanyList = responce.Content.ReadFromJsonAsync<List<Company>>().Result;

            CompanyList.Insert(0, new Company()
            {
                Name = "Выберите компанию"
            });

            CompanyBox.ItemsSource = CompanyList;
            CompanyBox.SelectedIndex = 0;
        }

        private async void ClientSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var newClient = new Client();

                newClient.FullName = FIOText.Text;
                newClient.Phone = PhoneNumber.Text;
                newClient.PasSeries = PassportSeries.Text;
                newClient.PasNumber = PassportNumber.Text;
                newClient.Mail = Email.Text;
                newClient.BirthDate = BirthDatePicker.SelectedDate;

                if(CompanyBox.SelectedIndex > 0)
                {
                    newClient.Company = (CompanyBox.SelectedItem as Company).Id;
                }

                var client = new HttpClient();
                var json = JsonSerializer.Serialize(newClient);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync("http://localhost:5080/api/PostClients", jsonContent);

                MessageBox.Show("Пользователь успешно сохранен");

                NavigationClass.MainNavigation.Content = new OrderEditorPage(number);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
            }
        }
    }
}
