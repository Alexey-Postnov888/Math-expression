using System.Windows;
using RpnLogic;

namespace RpnWpfApp
{

    class Point(double x, double y)
    {
        public readonly double X = x;
        public readonly double Y = y;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CanvasGraph.Children.Clear();
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            string input = txtboxInput.Text;
            double start = double.Parse(txtboxStart.Text);
            double end = double.Parse(txtboxEnd.Text);
            double scale = double.Parse(txtboxScale.Text);
            double step = double.Parse(txtboxStep.Text);

            var canvasGraph = CanvasGraph;
            
            var drawChart = new DrawChart(canvasGraph, start, end, step, scale);
            drawChart.DrawAxis();

            var calculator = new RpnCalculator(input);
            List<Point> points = new List<Point>();
            
            for (double x = start; x <= end; x += step)
            {
                var y = calculator.CalculateWithX(calculator.Rpn, x);
                points.Add(new Point(x, y));
            }
            
            drawChart.DrawGraph(points);
        }
    }
}