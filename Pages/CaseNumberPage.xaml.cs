using API.Models;
using Desktop.Classes;
using Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Логика взаимодействия для CaseNumberPage.xaml
    /// </summary>
    public partial class CaseNumberPage : Page
    {
        public class CaseInfo
        {
            public List<Client> Clients { get; set; }
            public List<Company> Companies { get; set; }
            public List<Service> Services { get; set; }
            public int NextId { get; set; }
            public int NextNumber { get; set; }
        }

        private CaseInfo allInfo;

        public CaseNumberPage()
        {
            InitializeComponent();

            GetInfo();
        }

        public async void GetInfo()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/GetCaseInfo");
            allInfo = responce.Content.ReadFromJsonAsync<CaseInfo>().Result;

            FirstCaseNumberText.Text = allInfo.NextId.ToString();
            SecondCaseNumberText.Text = DateTime.Now.ToString("d").Replace(".", "");
            ThirdCaseNumberText.Text = allInfo.NextNumber.ToString();
        }

        private void EnterCaseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationClass.MainNavigation.Content = new OrderEditorPage(Convert.ToInt32(ThirdCaseNumberText.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте введенные данные");
            }
            
        }
    }
}
