using Mobile.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();

            GetNews();
        }

        public async void GetNews()
        {
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync("http://192.168.3.2:5080/api/GetNews");
            var json = JsonConvert.DeserializeObject<List<News>>(responce);

            NewsList.ItemsSource = json;
        }

        private void ExitClick(object sender, EventArgs e)
        {
            if (NavigationClass.AutClient == null)
            {
                NavigationClass.PresentPage.Content = new GuestPage().Content;
            }
            else
            {
                NavigationClass.PresentPage.Content = new UserPage().Content;
            }
        }
    }
}