using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Rg.Plugins.Popup.Pages;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankingPage : PopupPage
    {        
        public RankingPage()
        {
            InitializeComponent();
        }
        public static readonly SKColor TextColor = SKColors.Black;

        public static Chart[] CreateXamarinSample()
        {
            var entries = new[]
            {
                new Microcharts.Entry(33.4f)
                {
                    Label = "Java Script",
                    ValueLabel = "100.4M",
                    Color = SKColor.Parse("#5F9F9F "),
                    TextColor = TextColor
                },
                new Microcharts.Entry(95.1f)
                {
                    Label = "Java",
                    ValueLabel = "95.1M",
                    Color = SKColor.Parse("#98F5FF "),
                      TextColor = TextColor
                },
                new Microcharts.Entry(42.3f)
                {
                    Label = "Python",
                    ValueLabel = "78.3M",
                    Color = SKColor.Parse("#8EE5EE "),
                      TextColor = TextColor
                },
                new Microcharts.Entry(74.4f)
                {
                    Label = "C#",
                    ValueLabel = "74.4M",
                    Color = SKColor.Parse("#7AC5CD "),
                    TextColor = TextColor
                },
                new Microcharts.Entry(74.4f)
                {
                    Label = "PHP",
                    ValueLabel = "69.4M",
                    Color = SKColor.Parse("#53868B "),
                    TextColor = TextColor
                }
            };

            return new Chart[]
            {
                new BarChart()
                {
                  Entries = entries ,
                  LabelTextSize = 35
                },
                new PointChart()
                 {
                  Entries = entries ,
                  LabelTextSize = 35
                  },
                new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 8,
                    PointMode = PointMode.Square,
                    PointSize = 18,
                    LabelTextSize = 35
                },
                new DonutChart()
                { Entries = entries,
                  LabelTextSize = 35
                },
                new RadialGaugeChart()
                { Entries = entries ,
                  LabelTextSize = 25
                },
                new RadarChart()
                {
                  Entries = entries ,
                  LabelTextSize = 15,
                  Margin = 30
                },
            };
        }
        protected override void OnAppearing()
        {
            var charts = CreateXamarinSample();
            chart4.Chart = charts[5];

        }
    }
}