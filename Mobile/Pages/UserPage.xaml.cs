using Java.Util.Prefs;
using Mobile.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentView
    {
        public UserPage()
        {
            InitializeComponent();

            FullNameLabel.Text = NavigationClass.AutClient.FullName;
        }

        private void ServiceClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new ServicePage().Content;
        }

        private void NewsClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new NewsPage().Content;
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Xamarin.Essentials.Preferences.Clear();

            NavigationClass.PresentPage.Content = new MainPage().Content;
        }

        private void ProfileClick(object sender, EventArgs e)
        {
            NavigationClass.PresentPage.Content = new ProfilePage().Content;
        }
    }
}