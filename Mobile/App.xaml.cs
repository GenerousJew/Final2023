﻿using Mobile.Classes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = NavigationClass.PresentPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
