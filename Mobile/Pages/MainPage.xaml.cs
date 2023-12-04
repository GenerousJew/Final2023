using Android.OS;
using Android.Preferences;
using Mobile.Classes;
using Mobile.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if(Preferences.ContainsKey("SaveLogin"))
            {
                Autorization(Preferences.Get("SaveLogin", ""), Preferences.Get("SavePassword", ""));
            }
        }

        private async void Autorization(string login, string password)
        {
            try
            {
                var client = new HttpClient();
                var responce = await client.GetStringAsync("http://192.168.3.2:5080/api/Client/login=" + login + "&password=" + password);

                var user = JsonConvert.DeserializeObject<Client>(responce);

                if (user != null)
                {
                    NavigationClass.AutClient = user;

                    Preferences.Set("SaveLogin", user.Login);
                    Preferences.Set("SavePassword", user.Password);

                    NavigationClass.PresentPage.Content = new UserPage();
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Неправильный логин или пароль", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Неправильный логин или пароль", "Ok");
            }
        }

        private async void EnterClick(object sender, EventArgs e)
        {
            Autorization(LoginEditor.Text, PasswordEditor.Text);            
        }

        private void GuestEnterClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new GuestPage().Content;
        }

        private void RegistrateClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new RegistratePage().Content;
        }
    }
}
