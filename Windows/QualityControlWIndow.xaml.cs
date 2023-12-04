using API.Classes;
using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для QualityControlWIndow.xaml
    /// </summary>
    public partial class QualityControlWIndow : Window
    {
        public QualityControlWIndow()
        {
            InitializeComponent();

            GetResults();
        }

        public async void GetResults()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:5080/api/GetResults?serviceCode=258");
            var results = responce.Content.ReadFromJsonAsync<JsonResult>().Result;

            XText.Text = results.X.ToString() + " %";
            SText.Text = results.S.ToString();

            var chartValues = new ChartValues<decimal>();
            chartValues.AddRange(results.ResultDict.Values);

            SeriesCollection series = new SeriesCollection()
            {
                new LineSeries
                {
                    Values = chartValues
                }
            };

            AxesCollection axesY = new AxesCollection()
            {
                new Axis()
                {
                    
                    Position = AxisPosition.LeftBottom
                },
                new Axis()
                {
                    Labels = new List<string>() 
                    {
                        "+3S",
                        "+2S",
                        "+1S",
                        "X",
                        "-1S",
                        "-2S",
                        "-3S"
                    },
                    Position = AxisPosition.RightTop
                }
            };

            AxesCollection axesX = new AxesCollection()
            { 
                new Axis()
                {
                    Labels = results.ResultDict.Keys.ToList().ConvertAll(x => x.ToString()),
                    LabelsRotation = -50,
                    
                }
            };

            Diagram.Series = series;
            Diagram.AxisY = axesY;
            Diagram.AxisX = axesX;
        }
    }
}
