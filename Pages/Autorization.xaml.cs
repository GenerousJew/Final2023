using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        int enterCount = 0;
        string CaptchaText = "";

        public Autorization(bool isBlock)
        {
            InitializeComponent();

            if (isBlock)
            {
                GetBlock();
            }

#if DEBUG 
            LoginText.Text = "srobken8";
            PasswordTextHidden.Password = "Cbmj3Yi";
            PasswordTextVisible.Text = "Cbmj3Yi";
#endif
        }

        private async void GetBlock()
        {
            MessageBox.Show("Время сеанса истекло. Необходимо провести кварцевание помещения. Следующая сессия будет доступна через 15 минут.");

            EnterButton.IsEnabled = false;

            await Task.Delay(60000);

            EnterButton.IsEnabled = true;
        }

        private void GetCaptcha()
        {
            CaptchaPanel.Visibility = Visibility.Visible;

            Random random = new Random();
            string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            CaptchaText = Alphabet[random.Next(0, Alphabet.Length)].ToString() +
                Alphabet[random.Next(0, Alphabet.Length)].ToString() +
                Alphabet[random.Next(0, Alphabet.Length)].ToString() +
                Alphabet[random.Next(0, Alphabet.Length)].ToString();

            FirstCaptcha.Margin = new Thickness(0, random.Next(-8, 8), 0, 0);
            FirstCaptcha.Visibility = Visibility.Visible;
            FirstCaptcha.Text = CaptchaText[0].ToString();
            SecondCaptcha.Margin = new Thickness(0, random.Next(-8, 8), 0, 0);
            SecondCaptcha.Visibility = Visibility.Visible;
            SecondCaptcha.Text = CaptchaText[1].ToString();
            ThirdCaptcha.Margin = new Thickness(0, random.Next(-8, 8), 0, 0);
            ThirdCaptcha.Visibility = Visibility.Visible;
            ThirdCaptcha.Text = CaptchaText[2].ToString();
            FourthCaptcha.Margin = new Thickness(0, random.Next(-8, 8), 0, 0);
            FourthCaptcha.Visibility = Visibility.Visible;
            FourthCaptcha.Text = CaptchaText[3].ToString();
        }

        private void PasswordCheck(object sender, RoutedEventArgs e)
        {
            if(PasswordVis.IsChecked == true)
            {
                PasswordTextVisible.Text = PasswordTextHidden.Password;

                PasswordTextVisible.Visibility = Visibility.Visible;
                PasswordTextHidden.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordTextHidden.Password = PasswordTextVisible.Text;

                PasswordTextVisible.Visibility = Visibility.Hidden;
                PasswordTextHidden.Visibility = Visibility.Visible;
            }
        }

        private async void EnterClick(object sender, RoutedEventArgs e)
        {
            if (LoginText.Text == "")
            {
                MessageBox.Show("Введите логин");
            }
            else if(PasswordTextHidden.Password == "" && PasswordTextVisible.Text == "")
            {
                MessageBox.Show("Введите пароль");
            }
            else if(CaptchaBox.Text != CaptchaText && CaptchaText != "")
            {
                MessageBox.Show("Captcha введена неправильно. Возможность входа заблокирована на 10 секунд.");

                EnterButton.IsEnabled = false;

                await Task.Delay(10000);

                EnterButton.IsEnabled = true;
            }
            else
            {
                string password;

                if (PasswordVis.IsChecked == true)
                {
                    password = PasswordTextVisible.Text;
                }
                else
                {
                    password = PasswordTextHidden.Password;
                }

                var host = Dns.GetHostName();
                string ip = Dns.GetHostByName(host).AddressList[1].ToString();

                var client = new HttpClient();
                var responce = await client.GetAsync("http://localhost:5080/api/GetAutorization/login=" + LoginText.Text + "&password=" + password + "&ip=" + ip);
                
                try
                {
                    var staff = await responce.Content.ReadFromJsonAsync<Staff>();

                    switch (staff.Type)
                    {
                        case 1:
                            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new AdminPage(staff);
                            break;
                        case 2:
                            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new LabPage(staff);
                            break;
                        case 3:
                            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new LabResPage(staff);
                            break;
                        case 4:
                            (Application.Current.MainWindow.FindName("PageNow") as Frame).Content = new AccountantPage(staff);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Пользователь с такими данными не найден. Проверьте правильность данных и пройдите дополнительную проверку.");
                    GetCaptcha();
                }
            }
        }

        private void CaptchaUpdate(object sender, RoutedEventArgs e)
        {
            GetCaptcha();
        }
    }
}
