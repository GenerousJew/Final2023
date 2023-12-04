using Mobile.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistratePage : ContentPage
    {
        public RegistratePage()
        {
            InitializeComponent();
        }

        private async void RegistrateClick(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();

                var newUser = new Client()
                {
                    FullName = FullNameEditor.Text,
                    Phone = PhoneEditor.Text,
                    Mail = MailEditor.Text,
                    BirthDate = BirthDatePicker.Date,
                    Login = LoginEditor.Text,
                    Password = PasswordEditor.Text
                };

                var json = JsonConvert.SerializeObject(newUser);

                var responce = await client.PostAsync("http://192.168.3.2:5080/api/Client", new StringContent(json, Encoding.UTF8, "application/json"));

                Preferences.Set("SaveLogin", newUser.Login);
                Preferences.Set("SavePassword", newUser.Password);

                NavigationClass.PresentPage.Content = new MainPage().Content;
            }
            catch (Exception)
            {
                await DisplayAlert("Ошибка!", "Проверьте правильность введенных данных и повторите попытку", "Ok");
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new MainPage().Content;
        }
    }
}