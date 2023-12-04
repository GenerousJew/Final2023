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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            FullNameLabel.Text = NavigationClass.AutClient.FullName;
            try
            {
                DateLabel.Text = NavigationClass.AutClient.BirthDate.ToString();
                AgeLabel.Text = (((TimeSpan)(DateTime.Now - NavigationClass.AutClient.BirthDate)).TotalDays / 365).ToString();
            }
            catch (Exception)
            {
                DateLabel.Text = "???";
                AgeLabel.Text = "???";
            }
            PhoneEditor.Text = NavigationClass.AutClient.Phone;
            MailEditor.Text = NavigationClass.AutClient.Mail;
            PasswordEditor.Text = NavigationClass.AutClient.Password;
        }

        private async void SaveClick(object sender, EventArgs e)
        {
            NavigationClass.AutClient.Phone = PhoneEditor.Text;
            NavigationClass.AutClient.Mail = MailEditor.Text;
            NavigationClass.AutClient.Password = PasswordEditor.Text;

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(NavigationClass.AutClient);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var responce = await client.PutAsync("http://192.168.3.2:5080/api/Client/" + NavigationClass.AutClient.Id.ToString(), content);

            if(responce.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Успех!", "Данные успешно сохранены", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка!", "Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже", "OK");
            }

            NavigationClass.PresentPage.Content = new UserPage();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new UserPage().Content;
        }
    }
}