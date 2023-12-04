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
    public partial class ServicePage : ContentPage
    {
        public ServicePage()
        {
            InitializeComponent();

            GetServices();
        }

        public async void GetServices()
        {
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync("http://192.168.3.2:5080/api/Service");
            var json = JsonConvert.DeserializeObject<List<Service>>(responce);

            ServiceList.ItemsSource = json;
        }

        private void ExitClick(object sender, EventArgs e)
        {
            if(NavigationClass.AutClient == null)
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