using API.Models;
using Desktop.Classes;
using Desktop.Pages;
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
using System.Windows.Shapes;

namespace Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для CaseEnterWindow.xaml
    /// </summary>
    public partial class CaseEnterWindow : Window
    {
        public CaseEnterWindow()
        {
            InitializeComponent();

            CaseFrame.Content = new CaseNumberPage();

            NavigationClass.MainNavigation = CaseFrame;
        }
    }
}
