using it2a_spol_blind_map;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAPA_PROG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MapPoint> points = new List<MapPoint>()
        {
            new MapPoint { Name = "Praha", XPercent = 0.34, YPercent = 0.38 },
            new MapPoint { Name = "Brno", XPercent = 0.66, YPercent = 0.68 },
            new MapPoint { Name = "Ostrava", XPercent = 0.88, YPercent = 0.45 },
        };
        public static Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPoints();
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var point in points)
            {
                double x = MapImage.ActualWidth * point.XPercent;
                double y = MapImage.ActualHeight * point.YPercent;

                Button btn = new Button()
                {
                    Content = point.Name,
                    Tag = point
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x);
                Canvas.SetTop(btn, y);

                OverlayCanvas.Children.Add(btn);
            }
        }
        private MapPoint aktualniTarget = null;

        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aktualniTarget = points[random.Next(points.Count)];

            double xx = aktualniTarget.XPercent;
            double yy = aktualniTarget.YPercent;

            MessageBox.Show($"Hra začala! Najděte město na těchto souřadnicích: X: {xx} , Y: {yy}");
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (aktualniTarget == null)
            {
                MessageBox.Show("Nejdřív se musí kliknout do mapy, aby se spustila hra!");
                return;
            }

            Button btn = sender as Button;
            MapPoint kliknuteMesto = btn.Tag as MapPoint;

            if (kliknuteMesto == aktualniTarget)
            {
                MessageBox.Show($"Výhra! Bylo to {kliknuteMesto.Name}.");
                aktualniTarget = null;
            }
            else
            {
                MessageBox.Show($"Prohra! Kliknul jsi na {kliknuteMesto.Name}. Město bylo jiné.");
            }
        }
    }
}