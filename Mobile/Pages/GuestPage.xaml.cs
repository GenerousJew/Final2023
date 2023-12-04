using Mobile.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuestPage : ContentPage
    {
        public GuestPage()
        {
            InitializeComponent();
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
            NavigationClass.PresentPage.Content = new MainPage().Content;
        }
    }
}